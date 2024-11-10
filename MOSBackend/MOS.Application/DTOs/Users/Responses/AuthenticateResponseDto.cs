﻿using MOS.Domain.Entities.Users;

namespace MOS.Application.DTOs.Users.Responses;

public class AuthenticateResponseDto
{
    public UserResponseDto User { get; set; }
    public string Token { get; set; }
    
    public AuthenticateResponseDto(User user, string token)
    {
        User = new UserResponseDto(user);
        Token = token;
    }
}