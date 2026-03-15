using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.JwtService;
using MS.Infrastructure.Persistence;
using MS.Infrastructure.Repositories.AdminRepositories.CreateUser;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllFacilities;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllSpecialties;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllUsers;
using MS.Infrastructure.Repositories.AdminRepositories.GetFacilityById;
using MS.Infrastructure.Repositories.AdminRepositories.GetSpecialtyById;
using MS.Infrastructure.Repositories.AdminRepositories.GetUserById;
using MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.CreateSpecialty;
using MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.GetSpecialtyByName;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateFacility;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateSpecialty;
using MS.Infrastructure.Repositories.AdminRepositories.UpdateUser;
using MS.Infrastructure.Repositories.DoctorRepositories.CreateMedicalRecord;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAppointments;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.DoctorRepositories.GetMedicalRecordByAppointmentId;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateMedicalRecord;
using MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentByFacility;
using MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorByFacility;
using MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorDetailByFacility;
using MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorScheduleByFacility;
using MS.Infrastructure.Repositories.ManagerRepositories.GetPendingAppointmentPatient;
using MS.Infrastructure.Repositories.ManagerRepositories.GetSpecialtiesByFacilityId;
using MS.Infrastructure.Repositories.PatientRepositories.CreateNewAccount;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;
using MS.Infrastructure.Repositories.PatientRepositories.UpdatePassword;
using MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentReport;
using MS.Infrastructure.Repositories.PatientRepositories.GetPatientAppointment;
using MS.Infrastructure.Repositories.AdminRepositories.GetFacilityByEmail;
using MS.Infrastructure.Repositories.AdminRepositories.GetFacilityByPhone;
using MS.Infrastructure.Repositories.AdminRepositories.CreateFacility;
using MS.Infrastructure.Repositories.AdminRepositories.GetFacilityByName;

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
            // UnitOfWork
            services.AddScoped<IUnitOfWork<AppDbContext>, UnitOfWork<AppDbContext>>();
            // Register Infrastructure Services (e.g., JWT, Guid services)
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IGetUserByEmail, GetUserByEmailImpl>();
            // Register Repositories for data access
            // services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IGetDoctorAvailabilities, GetDoctorAvailabilitiesImpl>();
            services.AddScoped<IGetDoctorAppointments, GetDoctorAppointmentsImpl>();
            services.AddScoped<IGetDoctorByUserId, GetDoctorByUserIdImpl>();
            services.AddScoped<IGetAllUsers, GetAllUsersImpl>();
            services.AddScoped<IGetUserById, GetUserByIdImpl>();
            services.AddScoped<IGetAppointmentById, GetAppointmentByIdImpl>();
            services.AddScoped<IUpdateAppointment, UpdateAppointmentImpl>();
            services.AddScoped<ICreateUser, CreateUserImpl>();
            services.AddScoped<IUpdateUser, UpdateUserImpl>();
            services.AddScoped<IUpdatePassword, UpdatePasswordImpl>();
            services.AddScoped<IGetDoctorByFacility, GetDoctorByFacilityImpl>();
            services.AddScoped<IGetDoctorDetailByFacility, GetDoctorDetailByFacilityImpl>();
            services.AddScoped<IGetAllFacilities, GetAllFacilitiesImpl>();
            services.AddScoped<IGetDoctorScheduleByFacility, GetDoctorScheduleByFacilityImpl>();
            services.AddScoped<IGetFacilityByEmail, GetFacilityByEmailImpl>();
            services.AddScoped<IGetFacilityByPhone, GetFacilityByPhoneImpl>();
            services.AddScoped<IGetFacilityByName, GetFacilityByNameImpl>();
            services.AddScoped<ICreateFacility, CreateFacilityImpl>();
            services.AddScoped<IGetSpecialtiesByFacilityId, GetSpecialtiesByFacilityIdImpl>();
            services.AddScoped<ICreateAccount, CreateAccountImpl>();
            services.AddScoped<IGetMedicalRecordByAppointmentId, GetMedicalRecordByAppointmentIdImpl>();
            services.AddScoped<ICreateMedicalRecord, CreateMedicalRecordImpl>();
            services.AddScoped<IGetAppointmentByFacility, GetAppointmentByFacilityImpl>();
            services.AddScoped<IGetFacilityById, GetFacilityByIdImpl>();
            services.AddScoped<IUpdateFacility, UpdateFacilityImpl>();
            services.AddScoped<IGetPendingAppointmentPatient, GetPendingAppointmentPatientImpl>();
            services.AddScoped<IGetAllSpecialties,GetAllSpecialtiesImpl>();
            services.AddScoped<IUpdateMedicalRecord, UpdateMedicalRecordImpl>();
            services.AddScoped<ICreateSpecialty, CreateSpecialtyImpl>();
            services.AddScoped<IGetSpecialtyByName, GetSpecialtyByNameImpl>();
            services.AddScoped<IUpdateSpecialty, UpdateSpecialtyImpl>();
            services.AddScoped<IGetSpecialtyById, GetSpecialtyByIdImpl>();
            services.AddScoped<IGetAppointmentReport, GetAppointmentReportImpl>();
            services.AddScoped<IGetAppointmentsByPatientId, GetAppointmentsByPatientIdImpl>();
            return services;
        }
    }
}