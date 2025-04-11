namespace MOS.Domain.Entities.Resumes;

public class ResumePost : Entity<long>
{
    public long ResumeCompanyEntryId { get; set; }
    
    public string Name { get; set; } = "";
    public string? Description { get; set; }
    
    public DateOnly DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
}