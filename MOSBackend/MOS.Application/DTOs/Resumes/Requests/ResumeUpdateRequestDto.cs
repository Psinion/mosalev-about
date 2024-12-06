using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Requests;

public class ResumeUpdateRequestDto
{
    [Required]
    public long Id { get; set; }
    
    [Required]
    [StringLength(30)] 
    public string Title { get; set; } = "";

    [Required]
    [StringLength(30)] 
    public string FirstName { get; set; } = "";    
    
    [Required]
    [StringLength(30)] 
    public string LastName { get; set; } = "";
    
    [Required]
    [StringLength(30)]
    public string Email { get; set; } = "";
    
    public int Salary { get; set; }

    public CurrencyType CurrencyType { get; set; } = CurrencyType.Ruble;
    
    public string? About { get; set; }

    public List<ResumeCompanyEntryUpdateRequestDto> CompanyEntries { get; set; } = new();
}