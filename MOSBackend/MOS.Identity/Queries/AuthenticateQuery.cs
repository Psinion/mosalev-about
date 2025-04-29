using System.ComponentModel.DataAnnotations;
using MOS.Application.DTOs.Users.Responses;
using MOS.Application.OperationResults;
using MOS.Application.SQRS.Base;

namespace MOS.Identity.Queries;

public record AuthenticateQuery : IRequest<OperationResult<AuthenticateResponseDto>>
{
    [Required]
    [StringLength(30)] 
    public string UserName { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string Password { get; set; } = "";
}