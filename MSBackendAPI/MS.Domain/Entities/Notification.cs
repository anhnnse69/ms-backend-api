using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Types;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a notification sent to a user (patient, doctor, manager, or admin) for various system events
    /// </summary>
    public class Notification : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        // User receiving the notification (works for any user type)
        public Guid UserId { get; set; }
        public User? User { get; set; }

        // Related appointment (if applicable)
        public Guid? AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        // Notification details - multi-language support
        public string? TitleVi { get; set; }
        public string? TitleEn { get; set; }
        public string? ContentVi { get; set; }
        public string? ContentEn { get; set; }

        // Notification type and channel
        public NotificationType Type { get; set; }
        public NotificationChannel Channel { get; set; }

        // Read status
        public bool IsRead { get; set; } = false;
        public DateTimeOffset? ReadAt { get; set; }

        // Sent status
        public bool IsSent { get; set; } = false;
        public DateTimeOffset? SentAt { get; set; }

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}