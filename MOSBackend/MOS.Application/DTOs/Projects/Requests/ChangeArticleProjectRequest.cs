namespace MOS.Application.DTOs.Projects.Requests;

public record ChangeArticleProjectRequest
{
    public int? ProjectId { get; init; }
}