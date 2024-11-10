using Microsoft.AspNetCore.Mvc;

namespace MOS.Identity.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthorizeAttribute : TypeFilterAttribute
{
    private readonly string permission;
    
    public CustomAuthorizeAttribute(string permission) : base(typeof(CustomAuthorizeFilter))
    {
        Arguments = new object[] { permission };
    }
}