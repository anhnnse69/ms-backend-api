
using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a doctor in the system
    /// </summary>
    public class Doctor : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
    {
        public string DisplayName { get; set; } // For UI display
        public string FullName { get; set; }
        public string BioVi { get; set; }
        public string BioEn { get; set; }
        public string AcademicTitleVi { get; set; }
        public string AcademicTitleEn { get; set; }
        public string AvatarUrl { get; set; } // UI avatar
        public string PhotoUrl { get; set; } // For backward compatibility

        public double AverageRating { get; set; }
        public int RatingCount { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }
        public bool IsActive { get; set; }

        public ICollection<DoctorFacility> Facilities { get; set; } = new List<DoctorFacility>();
        public ICollection<DoctorAvailability> Availabilities { get; set; } = new List<DoctorAvailability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<DoctorLanguage> Languages { get; set; } = new List<DoctorLanguage>();

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
