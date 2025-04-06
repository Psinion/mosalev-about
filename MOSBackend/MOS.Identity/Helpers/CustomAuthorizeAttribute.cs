using Microsoft.AspNetCore.Authorization;

namespace MOS.Identity.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(string permission = "")
    {
        Policy = permission;
    }
}