using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOS.Application.Data.Services.Users;
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
        var locale = context.Request.Headers["Locale"].FirstOrDefault()?.Split(" ").Last();
        if (locale != null)
        {
            HandleLocale(context, credentialsService, locale);
        }
        
        await next(context);
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