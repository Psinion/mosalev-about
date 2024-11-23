using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MOS.Application.Data.Services.Users;
using MOS.Domain.Entities.Users;
using MOS.Domain.Enums;
using MOS.Identity.Helpers;

namespace MOS.Identity.Middlewares;

public class UserMiddleware
{
    private readonly RequestDelegate next;
    private readonly AuthSettings authSettings;

    public UserMiddleware(RequestDelegate next, IOptions<AuthSettings> authSettings)
    {
        this.next = next;
        this.authSettings = authSettings.Value;
    }

    public async Task Invoke(HttpContext context, ICredentialsService credentialsService)
    {
        var token = context.Request.Headers["AuthToken"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            await HandleTokenAsync(context, credentialsService, token);
        }
        
        var locale = context.Request.Headers["Locale"].FirstOrDefault()?.Split(" ").Last();
        if (locale != null)
        {
            HandleLocale(context, credentialsService, locale);
        }
        
        await next(context);
    }

    private async Task<bool> HandleTokenAsync(HttpContext context, ICredentialsService credentialsService, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authSettings.JwtSecretKey);

            var parameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            tokenHandler.ValidateToken(token, parameters, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var userId = long.Parse(jwtToken.Claims.First(x => x.Type == nameof(User.Id)).Value);
            context.Items["UserId"] = userId;
            
            await credentialsService.InitUserAsync(userId);
        }
        catch(Exception ex)
        {
            return false;
        }

        return true;
    }
    
    private bool HandleLocale(HttpContext context, ICredentialsService credentialsService, string locale)
    {
        try
        {
            var localeParsed = int.Parse(locale);
            if (localeParsed >= (int)Locale.Ru && localeParsed <= (int)Locale.En)
            {
                context.Items["locale"] = localeParsed;
                credentialsService.CurrentLocale = (Locale) localeParsed;
            }
        }
        catch(Exception ex)
        {
            return false;
        }

        return true;
    }
}