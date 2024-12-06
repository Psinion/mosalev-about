namespace MOS.Domain.Entities.Resumes;

public class ResumeEducation : Entity
{
    public string EducationLevel { get; set; } = "";
    public string OrganisationName { get; set; } = "";
    public string FacultyName { get; set; } = "";
    public string SpecializationName { get; set; } = "";
    public DateOnly DateEnd { get; set; }
}