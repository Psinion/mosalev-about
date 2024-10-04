using MOS.Data.EF.Access.Contexts;
using MOS.WebApi.Extensions;
using MOS.WebApi.StartupConfiguration;

namespace MOS.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .MigrateDatabase<MainDbContext>()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>()
            );
    }
}