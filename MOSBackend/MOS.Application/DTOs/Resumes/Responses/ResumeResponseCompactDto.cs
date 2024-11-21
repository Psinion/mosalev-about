using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Responses;

public class ResumeResponseCompactDto
{
    public long Id { get; set; }
    public string Title { get; set; } = "";
    public Locale? PinnedToLocale { get; set; }
}