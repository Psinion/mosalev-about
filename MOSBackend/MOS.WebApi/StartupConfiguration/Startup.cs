using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MOS.Data.EF.Access.Contexts;
using MOS.Identity.Middlewares;
using MOS.WebApi.StartupConfiguration.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

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
        
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SwaggerDefaultValues>();
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
        
        services.AddCors(options =>
        {
            var origins = configuration.GetValue<string>("AllowedOrigins");
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(origins)
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        
        /*services.AddHttpsRedirection(options =>
        {
            options.HttpsPort = 8081;
        });*/
        
        services
            .AddConfigurations(configuration)
            .AddRepositories()
            .AddServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);
                }
                options.RoutePrefix = string.Empty;
            });
        }
        
        app.UseRouting();

        app.UseCors();

        app.UseMiddleware<UserMiddleware>();
        
        app.UseEndpoints(endpoints =>
            endpoints.MapControllers()
        );
    }
}
