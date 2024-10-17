using MOS.Application.Data.Repositories.Users;
using MOS.Application.Data.Services.Users;
using MOS.Data.EF.Access.Repositories.Users;
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

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        
        return services;
    }
}