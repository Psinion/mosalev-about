using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Projects.Requests;

public record UpdateProjectRequest
{
    public long Id { get; init; }
    
    [Required]
    [StringLength(30)] 
    public string Title { get; init; } = "";

    public string Description { get; init; } = "";
}