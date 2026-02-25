using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class Patient : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
    {
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

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
