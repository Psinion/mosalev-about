using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Projects.Requests;

public record ProjectCreateRequestDto
{
    [Required]
    [StringLength(30)] 
    public string Title { get; set; } = "";
    
    [Required]
    public string Description { get; set; }
}