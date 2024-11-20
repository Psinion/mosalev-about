using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Users.Requests;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults.Enums;
using MOS.Identity.Helpers;

namespace MOS.WebApi.Controllers.v1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService usersService;
    private readonly ICredentialsService credentialsService;
    
    public UsersController(IUsersService usersService, ICredentialsService credentialsService)
    {
        this.usersService = usersService;
        this.credentialsService = credentialsService;
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
    
    [CustomAuthorize("users/verify")]
    [HttpGet]
    [Route("verify")]
    public ActionResult<VerifyResponseDto> Verify()
    {
        var user = credentialsService.CurrentUser;

        if (user == null)
        {
            return Unauthorized("incorrect_verify");
        }

        var result = new VerifyResponseDto(user);
        return Ok(result);
    }
}