using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorByUserId;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;

namespace MS.Application.Services.DoctorServices.UpdateDoctorAppointmentStatusService
{
    /// <summary>
    /// Service responsible for updating doctor appointment status
    /// </summary>
    public class UpdateDoctorAppointmentStatusService : IUpdateDoctorAppointmentStatusService
    {
        private readonly IGetDoctorByUserId _getDoctorByUserId;
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IUpdateAppointment _updateAppointment;

        /// <summary>
        /// Constructor for UpdateDoctorAppointmentStatusService
        /// </summary>
        /// <param name="getDoctorByUserId">Repository to retrieve doctor by user id</param>
        /// <param name="getAppointmentById">Repository to retrieve appointment by id</param>
        /// <param name="updateAppointment">Repository to update appointment</param>
        public UpdateDoctorAppointmentStatusService(
            IGetDoctorByUserId getDoctorByUserId,
            IGetAppointmentById getAppointmentById,
            IUpdateAppointment updateAppointment)
        {
            _getDoctorByUserId = getDoctorByUserId;
            _getAppointmentById = getAppointmentById;
            _updateAppointment = updateAppointment;
        }

        /// <summary>
        /// Process update appointment status request
        /// </summary>
        /// <param name="userId">User identifier extracted from JWT token</param>
        /// <param name="request">Update appointment status request</param>
        /// <returns>Update appointment status response</returns>
        public async Task<ApiResponse<UpdateDoctorAppointmentStatusResponse>> Process(Guid userId, UpdateDoctorAppointmentStatusRequest request)
        {
            // 1. Initialize validation flags
            bool isDoctorValid = true;
            bool isAppointmentValid = true;
            bool isOwnershipValid = true;
            bool isStatusTransitionValid = true;
            // 2. Retrieve doctor entity
            var retrievedDoctor = await RetrieveDoctor(userId);
            // 3. Retrieve appointment entity
            var retrievedAppointment = await RetrieveAppointment(request.AppointmentId);
            // 4. Validate retrieved data
            ValidateDoctor(retrievedDoctor, ref isDoctorValid);
            ValidateAppointment(retrievedAppointment, ref isAppointmentValid);
            ValidateOwnership(retrievedDoctor, retrievedAppointment, ref isOwnershipValid);
            ValidateStatusTransition(retrievedAppointment, request.Status, ref isStatusTransitionValid);
            // 5. Update appointment status
            await UpdateAppointmentStatus(retrievedAppointment, request.Status, isStatusTransitionValid);
            // 6. Create response
            return CreateResponse(
                retrievedAppointment,
                isDoctorValid,
                isAppointmentValid,
                isOwnershipValid,
                isStatusTransitionValid);
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
        /// Validate status transition according to business flow
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="newStatus">New appointment status</param>
        /// <param name="isStatusTransitionValid">Validation flag</param>
        private void ValidateStatusTransition(
            Appointment appointment,
            AppointmentStatus newStatus,
            ref bool isStatusTransitionValid)
        {
            if (appointment == null)
            {
                return;
            }
            var currentStatus = appointment.Status;
            isStatusTransitionValid = currentStatus switch
            {
                AppointmentStatus.PendingConfirmation =>
                    newStatus == AppointmentStatus.Confirmed ||
                    newStatus == AppointmentStatus.Cancelled,
                AppointmentStatus.Confirmed =>
                    newStatus == AppointmentStatus.CheckedIn ||
                    newStatus == AppointmentStatus.InProgress ||
                    newStatus == AppointmentStatus.Cancelled ||
                    newStatus == AppointmentStatus.NoShow,
                AppointmentStatus.CheckedIn =>
                    newStatus == AppointmentStatus.InProgress,
                AppointmentStatus.InProgress =>
                    newStatus == AppointmentStatus.Completed,
                _ => false
            };
        }

        /// <summary>
        /// Update appointment status
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <param name="status">New appointment status</param>
        /// <param name="isStatusTransitionValid">Status transition validation flag</param>
        private async Task UpdateAppointmentStatus(
            Appointment appointment,
            AppointmentStatus status,
            bool isStatusTransitionValid)
        {
            if (appointment != null && isStatusTransitionValid)
            {
                appointment.Status = status;
                await _updateAppointment.Execute(appointment);
            }
        }

        /// <summary>
        /// Map appointment entity to response model
        /// </summary>
        /// <param name="appointment">Appointment entity</param>
        /// <returns>Update appointment status response</returns>
        private UpdateDoctorAppointmentStatusResponse MapToResponse(Appointment appointment)
        {
            return new UpdateDoctorAppointmentStatusResponse
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
        /// <param name="isStatusTransitionValid">Status transition validation flag</param>
        /// <returns>API response</returns>
        private ApiResponse<UpdateDoctorAppointmentStatusResponse> CreateResponse(
            Appointment appointment,
            bool isDoctorValid,
            bool isAppointmentValid,
            bool isOwnershipValid,
            bool isStatusTransitionValid)
        {
            if (!isDoctorValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4011.ToString());
            }
            if (!isAppointmentValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4012.ToString());
            }
            if (!isOwnershipValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4014.ToString());
            }
            if (!isStatusTransitionValid)
            {
                return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                    .Fail(MessageCode.APP_MESSAGE_4013.ToString());
            }
            var result = MapToResponse(appointment);
            return ApiResponse<UpdateDoctorAppointmentStatusResponse>
                .Success(MessageCode.APP_MESSAGE_2007.ToString(), result);
        }
    }
}