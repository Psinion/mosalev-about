using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MOS.Data.EF.Access.Contexts;

namespace MOS.WebApi.StartupConfiguration;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "MOSBackend",
                Version = "v1" 
            });
        });
        
        services.AddDbContext<MainDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddControllersWithViews()
            .AddJsonOptions(options =>
            {
                options
                    .JsonSerializerOptions
                    .ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        
        services
            .AddConfigurations(configuration)
            .AddRepositories()
            .AddServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "MOS");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors();

        app.UseEndpoints(endpoints =>
            endpoints.MapControllers()
        );
    }
}
