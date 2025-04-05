namespace MOS.Application.DTOs.Projects.Requests;

public class ProjectResponseCompactDto
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}