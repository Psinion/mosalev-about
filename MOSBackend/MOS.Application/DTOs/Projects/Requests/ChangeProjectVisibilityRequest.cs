using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Projects.Requests;

public record ChangeProjectVisibilityRequest
{
    [Required] public bool Visible { get; init; }
}