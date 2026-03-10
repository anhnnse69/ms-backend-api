using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Types;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a notification sent to a patient, such as appointment reminders or status updates.
    /// </summary>
    public class Notification : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
    {
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid? AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public NotificationType Type { get; set; }     // Email, SMS, Push
        public NotificationChannel Channel { get; set; } // AppointmentReminder, StatusChanged, etc.
        public string Title { get; set; }
        public string Message { get; set; }

        public bool IsRead { get; set; }
        public DateTimeOffset? ReadAt { get; set; }
        public DateTimeOffset? SentAt { get; set; }
        public bool IsSent { get; set; }

        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}