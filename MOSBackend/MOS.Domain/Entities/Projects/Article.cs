namespace MOS.Domain.Entities.Projects;

public class Article : LoggedEntity
{
    public Project? Project { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}