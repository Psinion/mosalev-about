namespace MOS.Domain.Entities.Projects;

public class Project : LoggedEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
}