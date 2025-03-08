using MOS.Application.Data.Repositories.Index;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.IIndexService;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Repositories.Index;
using MOS.Data.EF.Access.Repositories.Resumes;
using MOS.Data.EF.Access.Repositories.Users;
using MOS.Data.EF.Access.Services.Index;
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
        services.AddScoped<IIndexContentsRepository, IndexContentsRepository>();

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICredentialsService, CredentialsService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IResumesService, ResumesService>();
        services.AddScoped<IResumeCompanyEntriesService, ResumeCompanyEntriesService>();
        services.AddScoped<IResumePostsService, ResumePostsService>();
        services.AddScoped<IIndexService, IndexService>();
        
        return services;
    }
}