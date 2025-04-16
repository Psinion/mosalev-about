using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Users.Requests;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.Mappings.Users;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Users;
using MOS.Identity.Helpers;

namespace MOS.Identity.Services;

public class UsersService : IUsersService
{
    private readonly AuthSettings authSettings;
    private readonly IUsersRepository usersRepository;
    
    public UsersService(IOptions<AuthSettings> authSettings, IUnitOfWork unitOfWork, IUsersRepository usersRepository)
    {
        this.authSettings = authSettings.Value;
        this.usersRepository = usersRepository;
    }
    
    public UsersService(AuthSettings authSettings, IUsersRepository usersRepository)
    {
        this.authSettings = authSettings;
        this.usersRepository = usersRepository;
    }
    
    public async Task<OperationResult<AuthenticateResponseDto>> AuthenticateAsync(AuthenticateRequestDto authenticateRequest)
    {
        var hashedPassword = HashPassword(authenticateRequest.Password, authSettings.AuthSalt);
        var user = await usersRepository.GetByCredentialsAsync(authenticateRequest.UserName, hashedPassword);

        if (user == null)
        {
            return OperationError.Unauthorized("incorrect_data", "Username or password is incorrect");
        }

        var token = GenerateJwtToken(user, authSettings.TokenDurationMinutes);

        return new AuthenticateResponseDto(user, token);
    }

    public async Task<List<UserResponseDto>> FilterByFioAsync(string? filter)
    {
        var users = await usersRepository.FilterByFioAsync(filter);
        return users.ToDtoList();
    }

    public void Dispose()
    {
        usersRepository.Dispose();
    }

    private string HashPassword(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var saltedPassword = string.Concat(password, salt);
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
    
    private string GenerateJwtToken(User user, int expireMinutes = 60)
    {
        var secretKey = Encoding.ASCII.GetBytes(authSettings.JwtSecretKey);
        var symmetricKey = new SymmetricSecurityKey(secretKey);
        var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}