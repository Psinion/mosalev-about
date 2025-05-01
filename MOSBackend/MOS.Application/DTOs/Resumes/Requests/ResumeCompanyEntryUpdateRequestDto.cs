using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Requests;

public class ResumeCompanyEntryUpdateRequestDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(80)] 
    public string Company { get; set; } = "";
    
    [StringLength(50)] 
    public string WebSiteUrl { get; set; } = "";    
    
    public string? Description { get; set; }
}