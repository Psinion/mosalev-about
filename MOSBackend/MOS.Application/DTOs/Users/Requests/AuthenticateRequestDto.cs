using System.ComponentModel.DataAnnotations;

namespace MOS.Application.DTOs.Users.Requests;

public class AuthenticateRequestDto
{
    [Required]
    [StringLength(30)] 
    public string UserName { get; set; } = "";

    [Required]
    [StringLength(30)]
    public string Password { get; set; } = "";
}