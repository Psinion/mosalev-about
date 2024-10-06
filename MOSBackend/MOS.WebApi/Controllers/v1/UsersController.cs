using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Users.Requests;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults.Enums;

namespace MOS.WebApi.Controllers.v1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService usersService;
    
    public UsersController(IUsersService usersService)
    {
        this.usersService = usersService;
    }
    
    [HttpPost]
    [Route("authenticate")]
    public async Task<ActionResult<AuthenticateResponseDto>> Authenticate(AuthenticateRequestDto request)
    {
        var response = await usersService.AuthenticateAsync(request);

        if (response.Error.ErrorType == ErrorType.Unauthorized)
        {
            return Unauthorized(response.Error);
        }

        return Ok(response.Value);
    }
}