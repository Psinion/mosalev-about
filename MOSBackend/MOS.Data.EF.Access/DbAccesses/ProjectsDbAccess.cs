using MOS.Application.Data.DbAccesses;
using MOS.Data.EF.Access.Contexts;
using MOS.Domain.Entities.Projects;

namespace MOS.Data.EF.Access.DbAccesses;

public class ProjectsDbAccess : IProjectsDbAccess
{
    private readonly MainDbContext context;

    public ProjectsDbAccess(MainDbContext context)
    {
        this.context = context;
    }
    
    public IQueryable<Project> GetProjects()
    {
        return context.Projects;
    }
}

public class ArticlesDbAccess : IArticlesDbAccess
{
    private readonly MainDbContext context;

    public ArticlesDbAccess(MainDbContext context)
    {
        this.context = context;
    }
    
    public IQueryable<Article> GetArticles()
    {
        return context.Articles;
    }
}