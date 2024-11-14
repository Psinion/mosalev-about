using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MOS.Application.Data.Services.Users;

namespace MOS.Identity.Helpers;

public class CustomAuthorizeFilter : IAsyncAuthorizationFilter
{
    private ICredentialsService credentialsService;
    private string permission;
    
    public CustomAuthorizeFilter(ICredentialsService credentialsService, string permission)
    {
        this.credentialsService = credentialsService;
        this.permission = permission;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userId = context.HttpContext.Items["UserId"];
        if (userId != null)
        {
            var userResult = await credentialsService.InitUserAsync((long)userId);
            if (userResult.IsSuccess)
            {
                return;
            }
        }
        
        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status403Forbidden };
    }
}