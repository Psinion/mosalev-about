using Microsoft.AspNetCore.Authorization;
using MOS.Application.Data;
using MOS.Application.Data.DbAccesses;
using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Files;
using MOS.Application.Data.Repositories.Projects;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Files;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Application.Modules.Files.Commands.Handlers;
using MOS.Application.Modules.Files.Queries.Handlers;
using MOS.Application.Modules.Projects.Commands.Handlers;
using MOS.Application.Modules.Projects.Queries.Handlers;
using MOS.Application.Modules.Users.Queries.Handlers;
using MOS.Data.EF.Access.DbAccesses;
using MOS.Data.EF.Access.Handlers.Articles;
using MOS.Data.EF.Access.Handlers.Files;
using MOS.Data.EF.Access.Handlers.Projects;
using MOS.Data.EF.Access.Helpers;
using MOS.Data.EF.Access.Repositories;
using MOS.Data.EF.Access.Repositories.Files;
using MOS.Data.EF.Access.Repositories.Projects;
using MOS.Data.EF.Access.Repositories.Resumes;
using MOS.Data.EF.Access.Repositories.Users;
using MOS.Data.EF.Access.Services.Resumes;
using MOS.Identity.Handlers;
using MOS.Identity.Helpers;
using MOS.Identity.Services;
using MOS.WebApi.Factories;
using MOS.WebApi.Services.Files;

namespace MOS.WebApi.StartupConfiguration;

public static class ServicesExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthSettings>(configuration.GetSection(nameof(AuthSettings)));
        services.Configure<FileStorageSettings>(configuration.GetSection(nameof(FileStorageSettings)));
        return services;
    }
    
    public static IServiceCollection AddDbAccesses(this IServiceCollection services)
    {
        services.AddScoped<IProjectsDbAccess, ProjectsDbAccess>();
        services.AddScoped<IArticlesDbAccess, ArticlesDbAccess>();
        services.AddScoped<IUploadedFilesDbAccess, UploadedFilesDbAccess>();

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
        services.AddScoped<IFilesStorageService, FilesStorageService>();
        
        services.AddTransient<IAuthorizationHandler, PermissionHandler>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IHandlerFactory, HandlerFactory>();
        
        services.AddScoped<ICredentialsService, CredentialsService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IResumesService, ResumesService>();
        services.AddScoped<IResumeCompanyEntriesService, ResumeCompanyEntriesService>();
        services.AddScoped<IResumePostsService, ResumePostsService>();
        services.AddScoped<IResumeSkillsService, ResumeSkillsService>();
        services.AddScoped<IUploadedFilesRepository, UploadedFilesRepository>();
        
        return services;
    }
    
    public static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticateHandler, AuthenticateHandler>();
        services.AddScoped<IGetCompactProjectsHandler, GetCompactProjectsHandler>();
        services.AddScoped<IGetProjectHandler, GetProjectHandler>();
        services.AddScoped<ICreateProjectHandler, CreateProjectHandler>();
        services.AddScoped<IUpdateProjectHandler, UpdateProjectHandler>();
        services.AddScoped<IDeleteProjectHandler, DeleteProjectHandler>();
        services.AddScoped<IChangeProjectVisibilityHandler, ChangeProjectVisibilityHandler>();
        services.AddScoped<IChangeArticleProjectHandler, ChangeArticleProjectHandler>();
            
        services.AddScoped<IGetArticleHandler, GetArticleHandler>();
        services.AddScoped<ICreateArticleHandler, CreateArticleHandler>();
        services.AddScoped<IUpdateArticleHandler, UpdateArticleHandler>();
        services.AddScoped<IGetCompactArticlesHandler, GetCompactArticlesHandler>();
        services.AddScoped<IGetCompactArticlesByProjectHandler, GetCompactArticlesByProjectHandler>();
        services.AddScoped<IDeleteArticleHandler, DeleteArticleHandler>();
        services.AddScoped<IChangeArticleVisibilityHandler, ChangeArticleVisibilityHandler>();
        
        services.AddScoped<ICreateFileHandler, CreateFileHandler>();
        services.AddScoped<IDeleteFileHandler, DeleteFileHandler>();
        services.AddScoped<IGetFileHandler, GetFileHandler>();
        services.AddScoped<IGetFilesHandler, GetFilesHandler>();
        
        return services;
    }
}