using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Users;

namespace MOS.WebApi.Controllers.v1;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    //private readonly IUsersService usersService;
    
    /*public UsersController(IUsersService usersService)
    {
        this.usersService = usersService;
    }
    
    [HttpPost()]
    [Route("authenticate")]
    public async Task<ActionResult<AuthenticateResponseDto>> Authenticate(AuthenticateRequestDto request)
    {
        var response = await usersService.AuthenticateAsync(request);

        if (response.Error.ErrorType == ErrorType.Unauthorized)
        {
            return Unauthorized(response.Error.Description);
        }

        return Ok(response.Value);
    }*/

    private readonly IUsersRepository usersRepository;

    public UsersController(IUsersRepository usersRepository)
    {
        this.usersRepository = usersRepository;
    }
    
    [HttpGet()]
    [Route("get_all")]
    public async Task<ActionResult> GetAll()
    {
        var response = await this.usersRepository.GetAll().ToListAsync();
        return Ok(response);
    }
}