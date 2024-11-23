using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MOS.Application.Data.Services.Users;
using MOS.Application.OperationResults;

namespace MOS.Identity.Helpers;

public class CustomAuthorizeFilter : IAuthorizationFilter
{
    private ICredentialsService credentialsService;
    private string permission;
    
    public CustomAuthorizeFilter(ICredentialsService credentialsService, string permission)
    {
        this.credentialsService = credentialsService;
        this.permission = permission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (credentialsService.CurrentUser != null)
        {
            return;
        }
        
        context.Result = new JsonResult(OperationError.Unauthorized()) { StatusCode = StatusCodes.Status403Forbidden };
    }
}