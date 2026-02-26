using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Roles;
using MS.Infrastructure.Persistence;
using System.Text;
using System.Text.Json;

namespace MS.API.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddApiServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        services.ConfigureDatabase(configuration);
        services.ConfigureJwt(configuration);
        services.ConfigureAuthorization();
        services.ConfigureCors();

        return services;
    }

    private static IServiceCollection ConfigureDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection") + ";TrustServerCertificate=True"
            )
        );
        return services;
    }

    private static IServiceCollection ConfigureJwt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSection = configuration.GetSection("Jwt");
        var secretKey = jwtSection["SecretKey"];

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSection["Issuer"],
                    ValidAudience = jwtSection["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey!)
                    ),
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
                        {
                            return context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                codeMessage = AuthMessageCode.APP_MESSAGE_0001.ToString()
                            }));
                        }

                        if (context.AuthenticateFailure is SecurityTokenExpiredException)
                        {
                            return context.Response.WriteAsync(JsonSerializer.Serialize(new
                            {
                                codeMessage = AuthMessageCode.APP_MESSAGE_0004.ToString()
                            }));
                        }

                        return context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            codeMessage = AuthMessageCode.APP_MESSAGE_0002.ToString()
                        }));
                    }
                };
            });

        return services;
    }

    private static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ITAdminOnly", policy =>
                policy.RequireRole(((int)SystemRole.ITAdmin).ToString()));

            options.AddPolicy("ManagerOnly", policy =>
                policy.RequireRole(((int)SystemRole.Manager).ToString()));

            options.AddPolicy("PatientOnly", policy =>
                policy.RequireRole(((int)SystemRole.Patient).ToString()));

            options.AddPolicy("AdminOrManager", policy =>
                policy.RequireRole(
                    ((int)SystemRole.ITAdmin).ToString(),
                    ((int)SystemRole.Manager).ToString()
                ));
        });

        return services;
    }

    private static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        return services;
    }
}