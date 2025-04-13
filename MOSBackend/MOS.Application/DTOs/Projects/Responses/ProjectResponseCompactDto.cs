namespace MOS.Application.DTOs.Projects.Responses;

public class ProjectResponseCompactDto
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}