namespace MOS.Domain.Entities.Resumes;

public class Company : LoggedEntity
{
    public string Name { get; set; } = "";

    public string? Commentary { get; set; }
}