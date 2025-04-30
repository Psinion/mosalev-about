using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MOS.Application.Data;
using MOS.Application.DTOs.Projects.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Projects.Commands;

public record UpdateProjectCommand : ICommand<OperationResult<ProjectDto>>
{
    public long Id { get; init; }
    public string Title { get; init; } = "";
    public string Description { get; init; } = "";
}