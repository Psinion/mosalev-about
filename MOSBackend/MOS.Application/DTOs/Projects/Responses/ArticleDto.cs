namespace MOS.Application.DTOs.Projects.Responses;

public record ArticleDto
{
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    
    public string Title { get; set; } = "";
    public string? Description { get; set; }
}