using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Projects.Requests;

public record ChangeArticleVisibilityRequest
{
    [Required] public bool Visible { get; init; }
}