using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MS.Application.Common.AnnotationValidationBehavior;
using MS.Application.Services.AdminServices.CreateDoctorService;
using MS.Application.Services.AdminServices.CreateFacilityService;
using MS.Application.Services.AdminServices.CreateSpecialtyService;
using MS.Application.Services.AdminServices.CreateUser;
using MS.Application.Services.AdminServices.DeleteDoctorService;
using MS.Application.Services.AdminServices.DeleteFacilityService;
using MS.Application.Services.AdminServices.DeleteSpecialtyService;
using MS.Application.Services.AdminServices.DeleteUserService;
using MS.Application.Services.AdminServices.GetAllDoctorsService;
using MS.Application.Services.AdminServices.GetAllFacilitiesService;
using MS.Application.Services.AdminServices.GetAllSpecialtiesService;
using MS.Application.Services.AdminServices.GetAllUsersService;
using MS.Application.Services.AdminServices.GetUserByIdService;
using MS.Application.Services.AdminServices.UpdateDoctorService;
using MS.Application.Services.AdminServices.UpdateFacilityService;
using MS.Application.Services.AdminServices.UpdateSpecialtyService;
using MS.Application.Services.AdminServices.UpdateUserService;
using MS.Application.Services.CommonServices.ForgotPasswordService;
using MS.Application.Services.CommonServices.LoginService;
using MS.Application.Services.CommonServices.RegisterService;
using MS.Application.Services.CommonServices.ResetPasswordService;
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
using MS.Application.Services.ManagerServices.AppointmentReportService;
using MS.Application.Services.ManagerServices.FacilitySpecialtyService;
using MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService;
using MS.Application.Services.ManagerServices.PendingAppointmentPatientService;
using MS.Application.Services.ManagerServices.SendNotificationService;
using MS.Application.Services.ManagerServices.UpdateFacilityService;
using MS.Application.Services.PatientServices.BookAppointmentService;
using MS.Application.Services.PatientServices.CancelAppointmentService;
using MS.Application.Services.PatientServices.ChangePasswordService;
using MS.Application.Services.PatientServices.GetDoctorDetailService;
using MS.Application.Services.PatientServices.GetPatientAppointment;
using MS.Application.Services.PatientServices.GetUserProfileService;
using MS.Application.Services.PatientServices.SearchDoctorService;
using MS.Application.Services.PatientServices.SubmitReviewService;
using MS.Application.Services.PatientServices.UpdateAppointment;
using MS.Application.Services.PatientServices.UpdateUserProfileService;

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
            services.AddScoped<IUpdateManagerFacilityService, UpdateManagerFacilityService>();
            services.AddScoped<IUpdateSpecialtyService, UpdateSpecialtyService>();
            services.AddScoped<IGetAppointmentReportService, GetAppointmentReportService>();
            services.AddScoped<IGetPatientAppointmentsService,  GetPatientAppointmentsService>();
            services.AddScoped<ICancelAppointmentService, CancelAppointmentService>();
            services.AddScoped<IDeleteSpecialtyService, DeleteSpecialtyService>();
            services.AddScoped<ICreateDoctorService, CreateDoctorService>();
            services.AddScoped<IGetAllDoctorsService, GetAllDoctorsService>();
            services.AddScoped<ISendNotificationService, SendNotificationService>();
            services.AddScoped<IUpdateDoctorService, UpdateDoctorService>();
            services.AddScoped<IRescheduleAppointmentService, RescheduleAppointmentService>();
            services.AddScoped<IGetFacilityPerformanceReportService, GetFacilityPerformanceReportService>();
            services.AddScoped<IDeleteDoctorService, DeleteDoctorService>();
            services.AddScoped<IGetUserProfileService, GetUserProfileService>();
            services.AddScoped<IUpdateUserProfileService, UpdateUserProfileService>();
            services.AddScoped<IBookAppointmentService, BookAppointmentService>();
            services.AddScoped<ISubmitReviewService, SubmitReviewService>();
            services.AddScoped<IGetDoctorDetailService, GetDoctorDetailService>();
            services.AddScoped<ISearchDoctorService, SearchDoctorService>();
            services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
            services.AddScoped<IResetPasswordService, ResetPasswordService>();
            // Register MediatR pipeline behaviors for Data Annotation validation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DataAnnotationValidationBehavior<,>));
            return services;
        }
    }
}