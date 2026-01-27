using MS.Domain.Enums.Roles;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public SystemRole Role { get; set; }

        public Guid? FacilityId { get; set; }
        public Facility Facility { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
