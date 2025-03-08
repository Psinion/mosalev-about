using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Resumes.Requests;

public class ResumePostCreateRequestDto
{
    [Required]
    public long ResumeCompanyEntryId { get; set; }
    
    [Required]
    [StringLength(50)] 
    public string Name { get; set; } = "";
    
    public string? Description { get; set; }
    
    [Required]
    public DateOnly DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
}