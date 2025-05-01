using MOS.Domain.Enums;

namespace MOS.Domain.Entities.Projects;

public class Project : AuditableEntity<long>
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public bool Visible { get; set; } = true;
    public Locale Locale { get; set; } = Locale.Ru;
}