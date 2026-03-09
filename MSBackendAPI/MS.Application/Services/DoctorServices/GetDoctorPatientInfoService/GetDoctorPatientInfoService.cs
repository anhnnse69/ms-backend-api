using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;

namespace MS.Application.Services.DoctorServices.GetDoctorPatientInfoService
{
    /// <summary>
    /// Service responsible for retrieving patient information for doctor
    /// </summary>
    public class GetDoctorPatientInfoService : IGetDoctorPatientInfoService
    {
        private readonly IGetDoctorByUserId _getDoctorByUserId;
        private readonly IGetAppointmentById _getAppointmentById;

        /// <summary>
        /// Constructor for GetDoctorPatientInfoService
        /// </summary>
        /// <param name="getDoctorByUserId">Repository to retrieve doctor by user id</param>
        /// <param name="getAppointmentById">Repository to retrieve appointment by id</param>
        public GetDoctorPatientInfoService(
            IGetDoctorByUserId getDoctorByUserId,
            IGetAppointmentById getAppointmentById)
        {
            _getDoctorByUserId = getDoctorByUserId;
            _getAppointmentById = getAppointmentById;
        }

        /// <summary>
        /// Process request to retrieve patient information
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Patient information response</returns>
        public async Task<ApiResponse<GetDoctorPatientInfoResponse>> Process(Guid userId, Guid appointmentId)
        {
            // 1. Initialize validation flags
            bool isDoctorValid = true;
            bool isAppointmentValid = true;
            bool isOwnershipValid = true;
            // 2. Retrieve doctor entity
            var retrievedDoctor = await RetrieveDoctor(userId);
            // 3. Retrieve appointment entity
            var retrievedAppointment = await RetrieveAppointment(appointmentId);
            // 4. Validate retrieved data
            ValidateDoctor(retrievedDoctor, ref isDoctorValid);
            ValidateAppointment(retrievedAppointment, ref isAppointmentValid);
            ValidateOwnership(retrievedDoctor, retrievedAppointment, ref isOwnershipValid);
            // 5. Create response
            return CreateResponse(
                retrievedAppointment,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid);
        }

        /// <summary>
        /// Retrieve doctor entity by user identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Doctor entity</returns>
        private async Task<Doctor> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        /// <summary>
        /// Retrieve appointment entity by identifier
        /// </summary>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Appointment entity</returns>
        private async Task<Appointment> RetrieveAppointment(Guid appointmentId)
        {
            return await _getAppointmentById.Execute(appointmentId);
        }

        /// <summary>
        /// Validate doctor existence
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="isDoctorValid">Validation flag</param>
        private void ValidateDoctor(Doctor doctor, ref bool isDoctorValid)
        {
            if (doctor == null)
            {
                isDoctorValid = false;
            }
        }

        /// <summary>
        /// Validate appointment existence
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isAppointmentValid">Validation flag</param>
        private void ValidateAppointment(Appointment appointment, ref bool isAppointmentValid)
        {
            if (appointment == null)
            {
                isAppointmentValid = false;
            }
        }

        /// <summary>
        /// Validate doctor ownership of the appointment
        /// </summary>
        /// <param name="doctor">Doctor entity</param>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isOwnershipValid">Validation flag</param>
        private void ValidateOwnership(
            Doctor doctor,
            Appointment appointment,
            ref bool isOwnershipValid)
        {
            if (doctor != null && appointment != null)
            {
                if (appointment.DoctorId != doctor.Id)
                {
                    isOwnershipValid = false;
                }
            }
        }

        /// <summary>
        /// Map patient entity to response model
        /// </summary>
        /// <param name="patient">Patient entity</param>
        /// <returns>Patient information response</returns>
        private GetDoctorPatientInfoResponse MapToResponse(Patient patient)
        {
            return new GetDoctorPatientInfoResponse
            {
                // Patient identifier
                PatientId = patient.Id,
                // Display name for UI
                DisplayName = patient.DisplayName,
                // Full name of the patient
                FullName = patient.FullName,
                // Date of birth of the patient
                DateOfBirth = patient.DateOfBirth,
                // Gender of the patient
                Gender = patient.Gender,
                // Contact phone number
                PhoneNumber = patient.PhoneNumber,
                // Email address
                Email = patient.Email,
                // Residential address
                Address = patient.Address,
                // Identity card number
                IdentityCard = patient.IdentityCard,
                // Health insurance number
                InsuranceNumber = patient.InsuranceNumber,
                // Avatar URL for UI display
                AvatarUrl = patient.AvatarUrl
            };
        }

        /// <summary>
        /// Create API response
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isDoctorValid">Doctor validation flag</param>
        /// <param name="isAppointmentValid">Appointment validation flag</param>
        /// <param name="isOwnershipValid">Ownership validation flag</param>
        /// <returns>API response</returns>
        private ApiResponse<GetDoctorPatientInfoResponse> CreateResponse(
            Appointment appointment,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid)
        {
            if (!isDoctorValid)
            {
                return ApiResponse<GetDoctorPatientInfoResponse>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            if (!isAppointmentValid)
            {
                return ApiResponse<GetDoctorPatientInfoResponse>
                    .Fail(MessageCode.APP_MESSAGE_4012.ToString());
            }
            if (!isOwnershipValid)
            {
                return ApiResponse<GetDoctorPatientInfoResponse>
                    .Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }
            var result = MapToResponse(appointment.Patient);
            return ApiResponse<GetDoctorPatientInfoResponse>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), result);
        }
    }
}