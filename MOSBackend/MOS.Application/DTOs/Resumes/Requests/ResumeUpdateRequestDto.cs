﻿using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Requests;

public class ResumeUpdateRequestDto
{
    [Required]
    [StringLength(30)] 
    public string Title { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string Email { get; set; } = "";
    
    [Required]
    public int Salary { get; set; }

    public CurrencyType CurrencyType { get; set; } = CurrencyType.Ruble;
}