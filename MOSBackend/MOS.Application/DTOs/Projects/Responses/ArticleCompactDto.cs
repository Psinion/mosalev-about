namespace MOS.Application.DTOs.Projects.Responses;

public record ArticleCompactDto
{
    public int Id { get; set; }
    public int? ProjectId { get; set; }
    public ProjectCompactDto? Project { get; set; }
    
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public bool Visible { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}