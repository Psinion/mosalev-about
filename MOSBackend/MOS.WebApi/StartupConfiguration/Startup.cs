using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MOS.Application.Data.Services.Users;
using MOS.Application.OperationResults;
using MOS.Data.EF.Access.Contexts;
using MOS.Identity.Helpers;
using MOS.Identity.Middlewares;
using MOS.WebApi.Extensions;
using MOS.WebApi.Middlewares;
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
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Description = "Example: \"Bearer {token}\"",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        
        services.AddDbContext<MainDbContext>((provider, options) =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), action =>
            {
                action.CommandTimeout(30);
            })
#if DEBUG
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
#endif
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
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["AuthSettings:JwtSecretKey"]!)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
                
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";
            
                        await context.Response.WriteAsJsonAsync(OperationError.Unauthorized());
                    },
                    OnTokenValidated = async context =>
                    {
                        var credentialsService = context.HttpContext.RequestServices
                            .GetRequiredService<ICredentialsService>();
                        
                        var userId = int.Parse(context.Principal.FindFirstValue(ClaimTypes.NameIdentifier));
                        
                        await credentialsService.InitUserAsync(userId);
                    }
                };
            });
        services.AddAuthorizationBuilder()
            .AddPolicy("PermissionPolicy", policy => policy.Requirements.Add(new PermissionRequirement("")));
        
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services
            .AddConfigurations(configuration)
            .AddDbAccesses()
            .AddRepositories()
            .AddServices()
            .AddHandlers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
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

        var uploadsPath = env.GetUploadsPath();
        Directory.CreateDirectory(uploadsPath);
        
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploadsPath),
            RequestPath = "/images"
        });
        
        app.UseExceptionHandler();
        
        app.UseRouting();

        app.UseCors();
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<UserMiddleware>();
        
        app.UseEndpoints(endpoints =>
            endpoints.MapControllers()
        );
    }
}
