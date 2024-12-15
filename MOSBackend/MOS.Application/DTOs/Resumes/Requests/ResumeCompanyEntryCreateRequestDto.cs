using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Requests;

public class ResumeCompanyEntryCreateRequestDto
{
    [Required]
    public long ResumeId { get; set; }
    
    [Required]
    [StringLength(50)] 
    public string Company { get; set; } = "";
    
    [StringLength(50)] 
    public string WebSiteUrl { get; set; } = "";    
    
    public string? Description { get; set; }
}