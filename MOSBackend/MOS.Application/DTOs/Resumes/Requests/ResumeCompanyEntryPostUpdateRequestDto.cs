using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Resumes.Requests;

public class ResumeCompanyEntryPostUpdateRequestDto
{
    [Required]
    public long Id { get; set; }
    
    [Required]
    [StringLength(30)] 
    public string Name { get; set; } = "";
    
    public string? Description { get; set; }
    
    public DateOnly DateStart { get; set; } 
    
    public DateOnly? DateEnd { get; set; } 
}