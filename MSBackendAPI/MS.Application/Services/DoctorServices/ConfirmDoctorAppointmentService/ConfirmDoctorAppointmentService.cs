using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;

namespace MS.Application.Services.DoctorServices.ConfirmDoctorAppointmentService
{
    /// <summary>
    /// Service responsible for confirming doctor appointment
    /// </summary>
    public class ConfirmDoctorAppointmentService : IConfirmDoctorAppointmentService
    {
        private readonly IGetDoctorByUserId _getDoctorByUserId;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IUpdateAppointment _updateAppointment;

        /// <summary>
        /// Constructor for ConfirmDoctorAppointmentService
        /// </summary>
        /// <param name="getDoctorByUserId">Repository to retrieve doctor by user id</param>
        /// <param name="getAppointmentById">Repository to retrieve appointment by id</param>
        /// <param name="updateAppointment">Repository to update appointment</param>
        public ConfirmDoctorAppointmentService(
            IGetDoctorByUserId getDoctorByUserId,
            IGetAppointmentById getAppointmentById,
            IUpdateAppointment updateAppointment)
        {
            _getDoctorByUserId = getDoctorByUserId;
            _getAppointmentById = getAppointmentById;
            _updateAppointment = updateAppointment;
        }

        /// <summary>
        /// Process confirm appointment request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="appointmentId">Appointment identifier</param>
        /// <returns>Confirm appointment response</returns>
        public async Task<ApiResponse<ConfirmDoctorAppointmentResponse>> Process(Guid userId, Guid appointmentId)
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
            // 5. Update appointment status
            await UpdateAppointment(retrievedAppointment, isDoctorValid, isAppointmentValid, isOwnershipValid);
            // 6. Create response
            return CreateResponse(retrievedAppointment, isDoctorValid, isAppointmentValid, isOwnershipValid);
        }

        /// <summary>
        /// Retrieve doctor by user identifier
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>Doctor entity</returns>
        private async Task<Doctor> RetrieveDoctor(Guid userId)
        {
            return await _getDoctorByUserId.Execute(userId);
        }

        /// <summary>
        /// Retrieve appointment entity
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
        /// Update appointment status to confirmed
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="isDoctorValid">Doctor validation flag</param>
        /// <param name="isAppointmentValid">Appointment validation flag</param>
        /// <param name="isOwnershipValid">Ownership validation flag</param>
        private async Task UpdateAppointment(
            Appointment appointment,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid)
        {
            if (isDoctorValid && isAppointmentValid && isOwnershipValid && appointment != null)
            {
                appointment.Status = AppointmentStatus.Confirmed;
                await _updateAppointment.Execute(appointment);
            }
        }

        /// <summary>
        /// Map appointment entity to response model
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <returns>Confirm appointment response</returns>
        private ConfirmDoctorAppointmentResponse MapToResponse(Appointment appointment)
        {
            return new ConfirmDoctorAppointmentResponse
            {
                // Appointment identifier
                AppointmentId = appointment.Id,
                // Updated appointment status
                Status = appointment.Status
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
        private ApiResponse<ConfirmDoctorAppointmentResponse> CreateResponse(
            Appointment appointment,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid)
        {
            if (!isDoctorValid)
            {
                return ApiResponse<ConfirmDoctorAppointmentResponse>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            if (!isAppointmentValid)
            {
                return ApiResponse<ConfirmDoctorAppointmentResponse>
                    .Fail(MessageCode.APP_MESSAGE_4012.ToString());
            }
            if (!isOwnershipValid)
            {
                return ApiResponse<ConfirmDoctorAppointmentResponse>
                    .Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }
            var result = MapToResponse(appointment);
            return ApiResponse<ConfirmDoctorAppointmentResponse>
                .Success(MessageCode.APP_MESSAGE_2005.ToString(), result);
        }
    }
}