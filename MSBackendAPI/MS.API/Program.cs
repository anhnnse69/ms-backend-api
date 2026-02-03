using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MS.API;
using MS.Application;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Roles;
using MS.Infrastructure;
using MS.Infrastructure.Persistence;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// 1. ADD SERVICES TO THE CONTAINER
// ==================================================================

builder.Services.AddControllers();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection") + ";TrustServerCertificate=True"
    )
);

// ==================================================================
// 2. AUTHENTICATION & JWT CONFIGURATION
// ==================================================================
var jwtSection = builder.Configuration.GetSection("Jwt");
var secretKey = jwtSection["SecretKey"];

builder.Services
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

                // 1. Token missing (APP_MESSAGE_0001)
                if (string.IsNullOrEmpty(context.Request.Headers["Authorization"]))
                {
                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        codeMessage = AuthMessageCode.APP_MESSAGE_0001.ToString()
                    }));
                }

                // 2. Token expired (APP_MESSAGE_0004)
                if (context.AuthenticateFailure is SecurityTokenExpiredException)
                {
                    return context.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        codeMessage = AuthMessageCode.APP_MESSAGE_0004.ToString()
                    }));
                }

                // 3. Token invalid / malformed (APP_MESSAGE_0002)
                return context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    codeMessage = AuthMessageCode.APP_MESSAGE_0002.ToString()
                }));
            }
        };
    });

// ==================================================================
// 3. AUTHORIZATION POLICIES
// ==================================================================
builder.Services.AddAuthorization(options =>
{
    // Policy for Admin
    options.AddPolicy("ITAdminOnly", policy =>
        policy.RequireRole(((int)SystemRole.ITAdmin).ToString()));

    // Policy for Manager
    options.AddPolicy("ManagerOnly", policy =>
        policy.RequireRole(((int)SystemRole.Manager).ToString()));

    // Policy for Admin or Manager
    options.AddPolicy("AdminOrManager", policy =>
        policy.RequireRole(
            ((int)SystemRole.ITAdmin).ToString(),
            ((int)SystemRole.Manager).ToString()
        ));
});

// ==================================================================
// 4. CORS CONFIGURATION
// ==================================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // URL Frontend Local
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// ==================================================================
// 5. REGISTER LAYERS (DI)
// ==================================================================
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// ==================================================================
// 6. CONFIGURE PIPELINE
// ==================================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }