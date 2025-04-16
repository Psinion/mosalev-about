using Microsoft.AspNetCore.Authorization;
using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Projects;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Repositories;
using MOS.Data.EF.Access.Repositories.Projects;
using MOS.Data.EF.Access.Repositories.Resumes;
using MOS.Data.EF.Access.Repositories.Users;
using MOS.Data.EF.Access.Services.Projects;
using MOS.Data.EF.Access.Services.Resumes;
using MOS.Identity.Helpers;
using MOS.Identity.Services;

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
        
        services.AddScoped<ICredentialsService, CredentialsService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IResumesService, ResumesService>();
        services.AddScoped<IResumeCompanyEntriesService, ResumeCompanyEntriesService>();
        services.AddScoped<IResumePostsService, ResumePostsService>();
        services.AddScoped<IResumeSkillsService, ResumeSkillsService>();
        services.AddScoped<IProjectsService, ProjectsService>();
        services.AddScoped<IArticlesService, ArticlesService>();
        
        return services;
    }
}