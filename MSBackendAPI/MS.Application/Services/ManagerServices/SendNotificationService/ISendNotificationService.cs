using MS.Application.Common.Response;

namespace MS.Application.Services.ManagerServices.SendNotificationService
{
    /// <summary>
    /// Interface for sending notification service
    /// </summary>
    public interface ISendNotificationService
    {
        /// <summary>
        /// Process the request to send a notification about an appointment
        /// </summary>
        Task<ApiResponse<SendNotificationResponse>> Process(SendNotificationRequest request);
    }
}