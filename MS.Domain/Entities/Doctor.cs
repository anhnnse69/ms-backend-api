using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a doctor in the system
    /// </summary>
    public class Doctor
    {
        [Key]
        public Guid Id { get; set; }

        public string FullName { get; set; }
        public string BioVi { get; set; }
        public string BioEn { get; set; }
        public string AcademicTitleVi { get; set; }
        public string AcademicTitleEn { get; set; }
        public string PhotoUrl { get; set; }

        public double AverageRating { get; set; }
        public int RatingCount { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<DoctorFacility> Facilities { get; set; } = new List<DoctorFacility>();
        public ICollection<DoctorAvailability> Availabilities { get; set; } = new List<DoctorAvailability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<DoctorLanguage> Languages { get; set; } = new List<DoctorLanguage>();
    }
}
