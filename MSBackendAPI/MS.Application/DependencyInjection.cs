using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.Common.AnnotationValidationBehavior;

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
            // services.AddScoped<IDoctorService, DoctorService>();
            // Register MediatR pipeline behaviors for Data Annotation validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DataAnnotationValidationBehavior<,>));
            return services;
        }
    }
}