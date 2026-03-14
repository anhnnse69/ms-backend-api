using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.Common.AnnotationValidationBehavior;
using MS.Application.Services.AdminServices.CreateFacilityService;
using MS.Application.Services.AdminServices.CreateUser;
using MS.Application.Services.AdminServices.DeleteUserService;
using MS.Application.Services.AdminServices.GetAllFacilitiesService;
using MS.Application.Services.AdminServices.GetAllSpecialtiesService;
using MS.Application.Services.AdminServices.GetAllUsersService;
using MS.Application.Services.AdminServices.GetUserByIdService;
using MS.Application.Services.AdminServices.UpdateFacilityService;
using MS.Application.Services.AdminServices.UpdateUserService;
using MS.Application.Services.CommonServices.LoginService;
using MS.Application.Services.CommonServices.RegisterService;
using MS.Application.Services.DoctorDetailByFacilityService;
using MS.Application.Services.Doctors.GetDoctorAppointmentService;
using MS.Application.Services.Doctors.GetDoctorAvailabilityService;
using MS.Application.Services.DoctorsByFacilityService;
using MS.Application.Services.DoctorScheduleService;
using MS.Application.Services.DoctorServices.ConfirmDoctorAppointmentService;
using MS.Application.Services.DoctorServices.CreateMedicalRecordService;
using MS.Application.Services.DoctorServices.GetDoctorPatientInfoService;
using MS.Application.Services.DoctorServices.RejectDoctorAppointmentService;
using MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService;
using MS.Application.Services.DoctorServices.UpdateDoctorMedicalRecordService;
using MS.Application.Services.ManagerServices.AppointmentByFacilityService;
using MS.Application.Services.PatientServices.ChangePasswordService;
using MS.Application.Services.ManagerServices.FacilitySpecialtyService;
using MS.Application.Services.ManagerServices.PendingAppointmentPatientService;
using MS.Application.Services.AdminServices.DeleteFacilityService;
using MS.Application.Services.AdminServices.CreateSpecialtyService;

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
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IGetDoctorAvailabilityService, GetDoctorAvailabilityService>();
            services.AddScoped<IGetDoctorAppointmentService, GetDoctorAppointmentService>();
            services.AddScoped<IGetAllUsersService, GetAllUsersService>();
            services.AddScoped<IGetUserByIdService, GetUserByIdService>();
            services.AddScoped<IConfirmDoctorAppointmentService, ConfirmDoctorAppointmentService>();
            services.AddScoped<IRejectDoctorAppointmentService, RejectDoctorAppointmentService>();
            services.AddScoped<ICreateUserService, CreateUserService>();
            services.AddScoped<IUpdateUserService, UpdateUserService>();
            services.AddScoped<IDeleteUserService, DeleteUserService>();
            services.AddScoped<IChangePasswordService, ChangePasswordService>();
            services.AddScoped<IChangePasswordService, ChangePasswordService>();
            services.AddScoped<IDoctorByFacilityService, DoctorByFacilityService>();
            services.AddScoped<IDoctorDetailByFacilityService, DoctorDetailByFacilityService>();
            services.AddScoped<IGetDoctorPatientInfoService, GetDoctorPatientInfoService>();
            services.AddScoped<IGetAllFacilitiesService, GetAllFacilitiesService>();
            services.AddScoped<ICreateFacilityService, CreateFacilityService>();
            services.AddScoped<IUpdateDoctorAppointmentStatusService, UpdateDoctorAppointmentStatusService>();
            services.AddScoped<IDoctorScheduleService, DoctorScheduleService>();
            services.AddScoped<IGetFacilitySpecialtiesService, GetFacilitySpecialtiesService>();
            services.AddScoped<ICreateMedicalRecordService, CreateMedicalRecordService>();
            services.AddScoped<IGetAppointmentByFacilityService, GetAppointmentByFacilityService>();
            services.AddScoped<IUpdateFacilityService, UpdateFacilityService>();
            services.AddScoped<IGetPendingAppointmentPatientService, GetPendingAppointmentPatientService>();
            services.AddScoped<IGetAllSpecialtiesService, GetAllSpecialtiesService>();
            services.AddScoped<IUpdateDoctorMedicalRecordService, UpdateDoctorMedicalRecordService>();
            services.AddScoped<IDeleteFacilityService, DeleteFacilityService>();
            services.AddScoped<ICreateSpecialtyService, CreateSpecialtyService>();
            // Register MediatR pipeline behaviors for Data Annotation validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DataAnnotationValidationBehavior<,>));
            return services;
        }
    }
}