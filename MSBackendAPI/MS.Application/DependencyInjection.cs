using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.Common.AnnotationValidationBehavior;
using MS.Application.Services.GetDoctorAppointmentService;
using MS.Application.Services.GetDoctorAvailabilityService;
using MS.Application.Services.LoginService;
using MS.Application.Services.UserService.GetAllUsersService;
using MS.Application.Services.UserService.GetUserByIdService;

namespace MS.Application
{
    /// <summary>
    /// Provides extension methods for registering application-layer services 
    /// such as business logic, MediatR, and validation behaviors.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registers application-level services and MediatR pipeline behaviors.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same <see cref="IServiceCollection"/> for method chaining.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register your Application Services here (e.g., Use Case handlers, domain services)
            // services.AddScoped<IPatientService, PatientService>();
             services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IGetDoctorAvailabilityService, GetDoctorAvailabilityService>();
            services.AddScoped<IGetDoctorAppointmentService, GetDoctorAppointmentService>();
             services.AddScoped<IGetAllUsersService, GetAllUsersService>();
             services.AddScoped<IGetUserByIdService, GetUserByIdService>();
            // Register MediatR pipeline behaviors for Data Annotation validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DataAnnotationValidationBehavior<,>));
            return services;
        }
    }
}