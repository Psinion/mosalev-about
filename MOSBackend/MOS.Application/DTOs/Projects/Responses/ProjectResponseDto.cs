namespace MOS.Application.DTOs.Projects.Responses;

public record ProjectResponseDto
{
    public long Id { get; set; }
    
    public string Title { get; set; } = "";
    
    public string? Description { get; set; }
}