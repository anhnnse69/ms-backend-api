using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Enums.Types;
using MS.Infrastructure.Repositories.DoctorRepositories.GetAppointmentById;
using MS.Infrastructure.Repositories.PatientRepositories.CancelAppointment;

namespace MS.Application.Services.PatientServices.CancelAppointmentService
{
    public class CancelAppointmentService : ICancelAppointmentService
    {
        private readonly IGetAppointmentById _getAppointmentById;
        private readonly ICancelAppointment _cancelAppointment;

        /// <summary>
        /// CancelAppointmentService constructor
        /// </summary>
        public CancelAppointmentService(
            IGetAppointmentById getAppointmentById,
            ICancelAppointment cancelAppointment)
        {
            _getAppointmentById = getAppointmentById;
            _cancelAppointment = cancelAppointment;
        }

        /// <summary>
        /// Process logic for cancelling an appointment
        /// </summary>
        /// <param name="request">The cancellation request data</param>
        /// <returns>ApiResponse indicating the result of the operation</returns>
        public async Task<ApiResponse<bool>> Proccess(CancelAppointmentRequest request)
        {
            // 1. Retrieve appointment by ID from the database
            var appointment = await RetrieveAppointmentData(request.Id);
            // 2. Validate if the appointment is eligible for cancellation
            var errorCode = ValidateCancellation(appointment);
            // 3. Update the entity status and save changes if valid
            await ExecuteCancellation(appointment, request, errorCode);
            // 4. Create and return the final API response
            return CreateResponse(errorCode);
        }

        /// <summary>
        /// Retrieve appointment from repository
        /// </summary>
        private async Task<Appointment> RetrieveAppointmentData(Guid id)
        {
            return await _getAppointmentById.Execute(id);
        }

        /// <summary>
        /// Validation logic: Check existence, timing, and current status
        /// </summary>
        private string ValidateCancellation(Appointment appointment)
        {
            if (appointment == null)
                return MessageCode.APP_MESSAGE_4012.ToString();
            if (appointment.AppointmentTime <= DateTimeOffset.UtcNow)
                return MessageCode.APP_MESSAGE_4013.ToString();
            if (appointment.Status == AppointmentStatus.Cancelled)
                return MessageCode.APP_MESSAGE_4013.ToString();
            return MessageCode.APP_MESSAGE_2000.ToString();
        }

        /// <summary>
        /// Execute the update operation if the validation passed
        /// </summary>
        private async Task ExecuteCancellation(Appointment appointment, CancelAppointmentRequest request, string errorCode)
        {
            if (errorCode == MessageCode.APP_MESSAGE_2000.ToString())
            {
                MappingToEntity(appointment, request);
                await _cancelAppointment.Execute(appointment);
            }
        }

        /// <summary>
        /// Mapping business fields from request to entity
        /// </summary>
        private void MappingToEntity(Appointment appointment, CancelAppointmentRequest request)
        {
            appointment.Status = AppointmentStatus.Cancelled;
            appointment.CancelledAt = DateTimeOffset.UtcNow;
            appointment.CancellationReason = request.Reason;
        }

        /// <summary>
        /// Create response object based on the result of the process
        /// </summary>
        private ApiResponse<bool> CreateResponse(string errorCode)
        {
            if (errorCode != MessageCode.APP_MESSAGE_2000.ToString())
            {
                return ApiResponse<bool>.Fail(errorCode);
            }
            return ApiResponse<bool>.Success(
                MessageCode.APP_MESSAGE_2004.ToString(),
                true
            );
        }
    }
}
