using Microsoft.AspNetCore.Authorization;
using MOS.Application.Data;
using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Projects;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.Modules.Users.Queries.Handlers;
using MOS.Data.EF.Access.Handlers;
using MOS.Data.EF.Access.Handlers.Projects;
using MOS.Data.EF.Access.Repositories;
using MOS.Data.EF.Access.Repositories.Projects;
using MOS.Data.EF.Access.Repositories.Resumes;
using MOS.Data.EF.Access.Repositories.Users;
using MOS.Data.EF.Access.Services.Projects;
using MOS.Data.EF.Access.Services.Resumes;
using MOS.Identity.Handlers;
using MOS.Identity.Helpers;
using MOS.Identity.Services;
using MOS.WebApi.Factories;

namespace MOS.WebApi.StartupConfiguration;

public static class ServicesExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IResumesRepository, ResumesRepository>();
        services.AddScoped<IResumeCompanyEntriesRepository, ResumeCompanyEntriesRepository>();
        services.AddScoped<IResumePostsRepository, ResumePostsRepository>();
        services.AddScoped<IResumeSkillsRepository, ResumeSkillsRepository>();
        services.AddScoped<IProjectsRepository, ProjectsRepository>();
        services.AddScoped<IArticlesRepository, ArticlesRepository>();

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationHandler, PermissionHandler>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IHandlerFactory, HandlerFactory>();
        
        services.AddScoped<ICredentialsService, CredentialsService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IResumesService, ResumesService>();
        services.AddScoped<IResumeCompanyEntriesService, ResumeCompanyEntriesService>();
        services.AddScoped<IResumePostsService, ResumePostsService>();
        services.AddScoped<IResumeSkillsService, ResumeSkillsService>();
        services.AddScoped<IArticlesService, ArticlesService>();
        
        return services;
    }
    
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticateHandler, AuthenticateHandler>();
        services.AddScoped<IGetCompactProjectsHandler, GetCompactProjectsHandler>();
        services.AddScoped<IGetProjectHandler, GetProjectHandler>();
        services.AddScoped<ICreateProjectHandler, CreateProjectHandler>();
        services.AddScoped<IUpdateProjectHandler, UpdateProjectHandler>();
        
        return services;
    }
}