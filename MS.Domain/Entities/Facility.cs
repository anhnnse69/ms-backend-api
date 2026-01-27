using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a medical facility (hospital, clinic, etc.)
    /// </summary>
    public class Facility
    {
        [Key]
        public Guid Id { get; set; }

        public string NameVi { get; set; }
        public string NameEn { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public FacilityType Type { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<DoctorFacility> DoctorFacilities { get; set; } = new List<DoctorFacility>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}