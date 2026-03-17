using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.DoctorRepositories.UpdateAppointment;

namespace MS.Application.Services.PatientServices.UpdateAppointment
{
    /// <summary>
    /// Service to handle the rescheduling of an appointment.
    /// Supports updating the date, the doctor, or both.
    /// </summary>
    public class RescheduleAppointmentService : IRescheduleAppointmentService
    {
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly IUpdateAppointment _updateAppointment;

        public RescheduleAppointmentService(
            IGetAppointmentById getAppointmentById,
            IUpdateAppointment updateAppointment)
        {
            _getAppointmentById = getAppointmentById;
            _updateAppointment = updateAppointment;
        }

        /// <summary>
        /// Main processing flow. Does not use 'if' statements in the outer scope to ensure total coverage.
        /// </summary>
        /// <param name="request">The reschedule request containing AppointmentId and new details.</param>
        /// <returns>A standardized API response.</returns>
        public async Task<ApiResponse<RescheduleAppointmentResponse>> Process(RescheduleAppointmentRequest request)
        {
            bool isAppointmentFound = true;
            bool isStatusValid = true;
            var retrievedAppointment = await RetrieveData(request.AppointmentId);
            ValidateData(retrievedAppointment, ref isAppointmentFound, ref isStatusValid);
            UpdateEntity(retrievedAppointment, request, isAppointmentFound, isStatusValid);
            await PersistChanges(retrievedAppointment, isAppointmentFound, isStatusValid);
            var responseData = Mapping(retrievedAppointment, isAppointmentFound, isStatusValid);
            return CreateResponse(responseData, isAppointmentFound, isStatusValid);
        }

        /// <summary>
        /// Retrieves appointment data from the repository.
        /// </summary>
        /// <param name="id">The Guid identifier of the appointment.</param>
        /// <returns>The Appointment entity.</returns>
        private async Task<Appointment> RetrieveData(Guid id) => await _getAppointmentById.Execute(id);

        /// <summary>
        /// Validates if the appointment exists and if the status allows rescheduling.
        /// </summary>
        /// <param name="appointment">The retrieved entity.</param>
        /// <param name="isFound">Reference flag for existence.</param>
        /// <param name="isValid">Reference flag for status validity.</param>
        private void ValidateData(Appointment appointment, ref bool isFound, ref bool isValid)
        {
            if (appointment == null) isFound = false;
            if (appointment != null && appointment.Status != AppointmentStatus.PendingConfirmation) isValid = false;
        }

        /// <summary>
        /// Updates the entity fields. Keeps existing values if request fields are null.
        /// </summary>
        /// <param name="app">The Appointment entity to update.</param>
        /// <param name="req">The request data.</param>
        /// <param name="isFound">Flag indicating if the appointment was found.</param>
        /// <param name="isValid">Flag indicating if the status is valid.</param>
        private void UpdateEntity(Appointment app, RescheduleAppointmentRequest req, bool isFound, bool isValid)
        {
            if (isFound && isValid)
            {
                app.AppointmentTime = req.NewAppointmentTime ?? app.AppointmentTime;
                app.DoctorId = req.NewDoctorId ?? app.DoctorId;
                app.LastModifiedBy = "System";
            }
        }

        /// <summary>
        /// Persists the updated entity through the infrastructure layer.
        /// </summary>
        /// <param name="app">The updated Appointment entity.</param>
        /// <param name="isFound">Flag for existence.</param>
        /// <param name="isValid">Flag for validity.</param>
        private async Task PersistChanges(Appointment app, bool isFound, bool isValid)
        {
            if (isFound && isValid) await _updateAppointment.Execute(app);
        }

        /// <summary>
        /// Separate mapping method to convert entity to DTO.
        /// </summary>
        /// <param name="app">The Appointment entity.</param>
        /// <param name="isFound">Flag for existence.</param>
        /// <param name="isValid">Flag for validity.</param>
        /// <returns>The response DTO or null if validation failed.</returns>
        private RescheduleAppointmentResponse Mapping(Appointment app, bool isFound, bool isValid)
        {
            if (!isFound || !isValid) return null;
            return new RescheduleAppointmentResponse
            {
                // Field: Appointment ID
                AppointmentId = app.Id,
                // Field: Updated Appointment Time
                UpdatedAppointmentTime = app.AppointmentTime,
                // Field: Updated Doctor ID
                UpdatedDoctorId = app.DoctorId
            };
        }

        /// <summary>
        /// Constructs the final API response based on validation flags.
        /// </summary>
        /// <param name="data">The mapped response DTO.</param>
        /// <param name="isFound">Flag for existence.</param>
        /// <param name="isValid">Flag for validity.</param>
        /// <returns>The API response wrapping the result or error message.</returns>
        private ApiResponse<RescheduleAppointmentResponse> CreateResponse(RescheduleAppointmentResponse data, bool isFound, bool isValid)
        {
            // Fail if appointment not found (Error 4012)
            if (!isFound) return ApiResponse<RescheduleAppointmentResponse>.Fail(MessageCode.APP_MESSAGE_4012.ToString());
            // Fail if status is invalid (Error 4013)
            if (!isValid) return ApiResponse<RescheduleAppointmentResponse>.Fail(MessageCode.APP_MESSAGE_4013.ToString());
            // Success (Code 2000)
            return ApiResponse<RescheduleAppointmentResponse>.Success(MessageCode.APP_MESSAGE_2000.ToString(), data);
        }
    }
}