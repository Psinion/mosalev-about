namespace MOS.Domain.Entities.Resumes;

public class ResumeCourse : Entity
{
    public string CourseName { get; set; } = "";
    public string OrganisationName { get; set; } = "";
    public DateOnly DateEnd { get; set; }
}