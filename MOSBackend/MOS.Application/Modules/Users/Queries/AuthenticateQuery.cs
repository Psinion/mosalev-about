using System.ComponentModel.DataAnnotations;
using MOS.Application.Data;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;

namespace MOS.Application.Modules.Users.Queries;

public record AuthenticateQuery : IQuery<OperationResult<AuthenticateResponseDto>>
{
    [Required]
    [StringLength(30)] 
    public string UserName { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string Password { get; set; } = "";
}