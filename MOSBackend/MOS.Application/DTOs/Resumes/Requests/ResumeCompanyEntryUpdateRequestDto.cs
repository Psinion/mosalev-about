using System.ComponentModel.DataAnnotations;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Requests;

public class ResumeCompanyEntryUpdateRequestDto
{
    [Required]
    public long Id { get; set; }
    
    [Required]
    [StringLength(50)] 
    public string Company { get; set; } = "";

    [Required]
    [StringLength(50)] 
    public string WebSiteUrl { get; set; } = "";    
    
    public string? Description { get; set; }

    public List<ResumeCompanyEntryPostUpdateRequestDto> ResumePosts { get; set; } = new();
}