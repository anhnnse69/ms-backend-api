using MS.API.Extensions;
using MS.Application;
using MS.Infrastructure;
using MS.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddAppConfigurations();
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

app.MigrateDatabase<AppDbContext>((_, _) => { });

app.UseApiServices();

app.Run();

public partial class Program { }