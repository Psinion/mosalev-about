using MOS.Domain.Entities.Resumes;
using MOS.Domain.Enums;

namespace MOS.Application.DTOs.Resumes.Responses;

public class ResumeResponseDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public int Salary { get; set; }
    public CurrencyType CurrencyType { get; set; }
}