using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Roles;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class User : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
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

        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginAt { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
