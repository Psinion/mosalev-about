using System.ComponentModel.DataAnnotations;
using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record CreateProjectCommand : ICommand<OperationResult<ProjectDto>>
{
    [Required]
    [StringLength(64)] 
    public string Title { get; init; } = "";

    public string Description { get; init; } = "";
}