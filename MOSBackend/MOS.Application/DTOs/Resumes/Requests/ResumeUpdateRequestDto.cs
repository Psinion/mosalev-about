using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Resumes.Requests;

public record ResumeUpdateRequestDto
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)] 
    public string Title { get; set; } = "";
    
    public string? About { get; set; }
}