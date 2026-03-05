using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Infrastructure.JwtService;
using MS.Infrastructure.Persistence;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.UserRepositories.GetUserByEmail;

namespace MS.Infrastructure
{
    /// <summary>
    /// Provides extension methods for registering infrastructure-level services 
    /// within the dependency injection container.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers infrastructure services, including database contexts and repositories.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> to retrieve connection strings.</param>
        /// <returns>The same <see cref="IServiceCollection"/> for method chaining.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext using SQL Server based on the connection string in configuration
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            // Register Infrastructure Services (e.g., JWT, Guid services)
             services.AddScoped<IJwtTokenService, JwtTokenService>();
             services.AddScoped<IGetUserByEmail, GetUserByEmailImpl>();
            // Register Repositories for data access
            // services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IGetDoctorAvailabilities, GetDoctorAvailabilitiesImpl>();
            services.AddScoped<IGetDoctorAppointments, GetDoctorAppointmentsImpl>();
            services.AddScoped<IGetDoctorByUserId, GetDoctorByUserIdImpl>();
            return services;
        }
    }
}