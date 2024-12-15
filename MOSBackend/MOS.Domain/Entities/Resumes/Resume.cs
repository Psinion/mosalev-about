using MOS.Domain.Enums;

namespace MOS.Domain.Entities.Resumes;

public class Resume : LoggedEntity
{
    public string Title { get; set; } = "";
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public int Salary { get; set; }
    public CurrencyType CurrencyType { get; set; } = CurrencyType.Ruble; 
    public string Email { get; set; } = "";
    
    public string? About { get; set; }
    public Locale? PinnedToLocale { get; set; }

    public List<ResumeCompanyEntry> CompanyEntries { get; set; } = new();
    public List<ResumeSkill> Skills { get; set; } = new();
    public List<ResumeEducation> Educations { get; set; } = new();
    public List<ResumeCourse> Courses { get; set; } = new();
}