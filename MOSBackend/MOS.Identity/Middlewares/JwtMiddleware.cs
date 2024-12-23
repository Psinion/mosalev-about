﻿using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MOS.Application.Data.Repositories.Users;
using MOS.Domain.Entities.Users;
using MOS.Identity.Helpers;

namespace MOS.Identity.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate next;
    private readonly AuthSettings authSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AuthSettings> authSettings)
    {
        this.next = next;
        this.authSettings = authSettings.Value;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["AuthToken"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            HandleToken(context, token);
        }

        await next(context);
    }

    private bool HandleToken(HttpContext context, string token)
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
        }
        catch(Exception ex)
        {
            return false;
        }

        return true;
    }
}