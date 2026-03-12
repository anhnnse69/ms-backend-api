using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Roles;

namespace MS.Domain.Entities
{
    public class User : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string DisplayName { get; set; } // For UI display
        public string FullName { get; set; }
        public string AvatarUrl { get; set; } // UI avatar
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public SystemRole Role { get; set; }

        public Guid? FacilityId { get; set; }
        public Facility Facility { get; set; }

        public DateTimeOffset? LastLoginAt { get; set; }

        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
