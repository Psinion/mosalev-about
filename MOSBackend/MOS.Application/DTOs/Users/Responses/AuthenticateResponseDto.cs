using MOS.Application.Mappings.Resumes;
using MOS.Application.Mappings.Users;
using MOS.Domain.Entities.Users;

namespace MOS.Application.DTOs.Users.Responses;

public class AuthenticateResponseDto
{
    public UserResponseDto User { get; set; }
    public string Token { get; set; }
    
    public AuthenticateResponseDto(User user, string token)
    {
        User = user.ToDto();
        Token = token;
    }
}

public class VerifyResponseDto
{
    public UserResponseDto User { get; set; }
    
    public VerifyResponseDto(User user)
    {
        User = user.ToDto();
    }
}