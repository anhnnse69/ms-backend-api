using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string IdentityCard { get; set; }
        public string InsuranceNumber { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
