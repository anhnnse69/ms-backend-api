using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Types;

namespace MS.Domain.Entities
{
    public class Patient : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        // Link to User account (1-1 relationship)
        public Guid? UserId { get; set; }
        public User? User { get; set; }

        public string DisplayName { get; set; } // For UI display
        public string FullName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
        public string InsuranceNumber { get; set; }
        public string AvatarUrl { get; set; } // UI avatar

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
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
