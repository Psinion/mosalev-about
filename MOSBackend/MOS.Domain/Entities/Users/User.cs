﻿using MOS.Domain.Entities.Base;

namespace MOS.Domain.Entities.Users;

public class User : Entity
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Patronymic { get; set; } = "";
    public string UserName { get; set; } = "";

    public string Password { get; set; } = "";
}