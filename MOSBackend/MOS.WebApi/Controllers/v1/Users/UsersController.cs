﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.Modules.Users;
using MOS.Application.Modules.Users.Queries;
using MOS.Application.Modules.Users.Queries.Handlers;
using MOS.Application.OperationResults;
using MOS.Application.OperationResults.Enums;
using MOS.Identity.Helpers;

namespace MOS.WebApi.Controllers.v1.Users;

[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IHandlerFactory handlerFactory;
    private readonly IUsersService usersService;
    private readonly ICredentialsService credentialsService;
    
    public UsersController(IHandlerFactory handlerFactory, IUsersService usersService, ICredentialsService credentialsService)
    {
        this.handlerFactory = handlerFactory;
        this.usersService = usersService;
        this.credentialsService = credentialsService;
    }
    
    [HttpPost]
    [Route("authenticate")]
    public async Task<ActionResult<AuthenticateResponseDto>> Authenticate(AuthenticateQuery request)
    {
        var authenticateHandler = handlerFactory.GetHandler<IAuthenticateHandler>();
        
        var response = await authenticateHandler.Handle(request);

        if (response.Error.ErrorType == ErrorType.Unauthorized)
        {
            return Unauthorized(response.Error);
        }

        return Ok(response.Value);
    }
    
    [CustomAuthorize]
    [HttpGet]
    [Route("verify")]
    public ActionResult<VerifyResponseDto> Verify()
    {
        var user = credentialsService.CurrentUser!;

        /*if (user == null)
        {
            return Unauthorized("incorrect_verify");
        }*/

        var result = new VerifyResponseDto(user);
        return Ok(result);
    }
    
    [CustomAuthorize]
    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<UserResponseDto>>> Users(string? fio = null)
    {
        var users = await usersService.FilterByFioAsync(fio);
        
        return Ok(users);
    }
}