using MOS.Application.Data.Users;
using MOS.Data.EF.Access.Repositories.Users;

namespace MOS.WebApi.StartupConfiguration;

public static class ServicesExtensions
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services;
    }
}