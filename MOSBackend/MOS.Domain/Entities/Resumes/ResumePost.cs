namespace MOS.Domain.Entities.Resumes;

public class ResumePost : Entity
{
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
}