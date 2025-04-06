using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MOS.Application.Data.Repositories.Users;

namespace MOS.Identity.Helpers;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUsersRepository usersRepository;

    public PermissionHandler(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return;
        }
        
        context.Succeed(requirement);

        /*var hasPermission = await _usersRepository.CheckPermissionAsync(
            long.Parse(userId), 
            requirement.Permission
        );

        if (hasPermission)
        {
            context.Succeed(requirement);
        }*/
    }
}