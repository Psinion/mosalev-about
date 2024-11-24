using MOS.Application.DTOs.Users.Responses;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Responses;

public class ResumeResponseCompactDto
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public Locale? PinnedToLocale { get; set; }
    
    public DateTime? DateCreate { get; set; }
    public UserResponseDto? UserCreate { get; set; }
    
    public DateTime? DateUpdate { get; set; }
    public UserResponseDto? UserUpdate { get; set; }
}