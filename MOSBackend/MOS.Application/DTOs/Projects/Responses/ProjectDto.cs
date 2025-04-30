namespace MOS.Application.DTOs.Projects.Responses;

public record ProjectDto
{
    public long Id { get; set; }
    
    public string Title { get; set; } = "";
    
    public string? Description { get; set; }
}