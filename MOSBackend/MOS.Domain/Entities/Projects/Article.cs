namespace MOS.Domain.Entities.Projects;

public class Article : AuditableEntity<long>
{
    public Project? Project { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool Visible { get; set; } = true;
}