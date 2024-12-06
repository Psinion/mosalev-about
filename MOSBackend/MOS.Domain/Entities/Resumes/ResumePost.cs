namespace MOS.Domain.Entities.Resumes;

public class ResumePost : Entity
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    
    public DateOnly DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
}