using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.ManagerRepositories.GetAppointmentByPatient;
using MS.Infrastructure.Repositories.ManagerRepositories.SendNotification;

namespace MS.Application.Services.ManagerServices.SendNotificationService
{
    /// <summary>
    /// Service implementation for sending notifications about appointments to patients
    /// </summary>
    public class SendNotificationService : ISendNotificationService
    {
        private readonly IGetAppointmentByPatient _getAppointmentByPatient;
        private readonly ISendNotificationRepository _sendNotificationRepository;

        /// <summary>
        /// Constructor for SendNotificationService
        /// </summary>
        /// <param name="getAppointmentByPatient"></param>
        /// <param name="sendNotificationRepository"></param>
        public SendNotificationService(
            IGetAppointmentByPatient getAppointmentByPatient,
            ISendNotificationRepository sendNotificationRepository)
        {
            _getAppointmentByPatient = getAppointmentByPatient;
            _sendNotificationRepository = sendNotificationRepository;
        }

        /// <summary>
        /// Process the request to send a notification
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResponse<SendNotificationResponse>> Process(SendNotificationRequest request)
        {
            // 1. Initialize validation flags
            bool isAppointmentValid = true;
            // 2. Retrieve appointment data
            var appointment = await RetrieveAppointment(request.PatientId, request.AppointmentId);
            // 3. Validate retrieved data
            ValidateAppointmentData(appointment, request.PatientId, ref isAppointmentValid);
            // 4. Create response
            return await CreateResponse(request, appointment, isAppointmentValid);
        }

        /// <summary>
        /// Retrieve appointment by patient ID and appointment ID
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        private async Task<Appointment?> RetrieveAppointment(Guid patientId, Guid appointmentId)
        {
            return await _getAppointmentByPatient.Execute(patientId, appointmentId);
        }

        /// <summary>
        /// Validate whether appointment exists and belongs to the patient
        /// </summary>
        /// <param name="appointment"></param>
        /// <param name="patientId"></param>
        /// <param name="isValid"></param>
        private void ValidateAppointmentData(Appointment? appointment, Guid patientId, ref bool isValid)
        {
            if (appointment == null || appointment.PatientId != patientId)
            {
                isValid = false;
            }
        }

        /// <summary>
        /// Build Notification entity from request and appointment data
        /// </summary>
        /// <param name="request"></param>
        /// <param name="appointment"></param>
        /// <returns></returns>
        private Notification BuildNotificationEntity(SendNotificationRequest request, Appointment appointment)
        {
            var userId = appointment.Patient?.UserId ?? Guid.Empty;

            return new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                AppointmentId = request.AppointmentId,
                TitleVi = request.TitleVi,
                TitleEn = request.TitleEn,
                ContentVi = request.ContentVi,
                ContentEn = request.ContentEn,
                Type = request.Type,
                Channel = request.Channel,
                IsRead = false,
                ReadAt = null,
                IsSent = true,
                SentAt = DateTimeOffset.UtcNow,
                IsDeleted = false,
                DeletedAt = null,
                DeletedBy = null,
                CreateBy = request.PatientId.ToString(),
                LastModifiedBy = request.PatientId.ToString(),
                CreateDate = DateTimeOffset.UtcNow,
                LastModifiedDate = DateTimeOffset.UtcNow
            };
        }

        /// <summary>
        /// Save notification to the database
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        private async Task<Notification> SaveNotification(Notification notification)
        {
            return await _sendNotificationRepository.Execute(notification);
        }

        /// <summary>
        /// Create response for the send notification request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="appointment"></param>
        /// <param name="isAppointmentValid"></param>
        /// <returns></returns>
        private async Task<ApiResponse<SendNotificationResponse>> CreateResponse(
            SendNotificationRequest request,
            Appointment? appointment,
            bool isAppointmentValid)
        {
            if (!isAppointmentValid)
            {
                var errorCode = appointment == null
                    ? MessageCode.APP_MESSAGE_4012.ToString()
                    : MessageCode.APP_MESSAGE_4010.ToString();
                return ApiResponse<SendNotificationResponse>.Fail(errorCode);
            }
            var notification = BuildNotificationEntity(request, appointment!);
            var savedNotification = await SaveNotification(notification);
            return ApiResponse<SendNotificationResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                new SendNotificationResponse(
                    savedNotification.Id,
                    savedNotification.IsSent,
                    savedNotification.SentAt,
                    "Notification sent to patient successfully"
                )
            );
        }
    }
}