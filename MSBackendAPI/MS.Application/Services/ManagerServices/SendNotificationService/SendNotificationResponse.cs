namespace MS.Application.Services.ManagerServices.SendNotificationService
{
    /// <summary>
    /// Response object for sending notifications to patients.
    /// </summary>
    public class SendNotificationResponse
    {
        /// <summary>Unique identifier of the sent notification.</summary>
        public Guid NotificationId { get; set; }

        /// <summary>Indicates whether the notification was sent successfully.</summary>
        public bool IsSent { get; set; }

        /// <summary>Timestamp when the notification was sent.</summary>
        public DateTimeOffset? SentAt { get; set; }

        public SendNotificationResponse()
        {
        }

        public SendNotificationResponse(Guid notificationId, bool isSent, DateTimeOffset? sentAt)
        {
            NotificationId = notificationId;
            IsSent = isSent;
            SentAt = sentAt;
        }
    }
}