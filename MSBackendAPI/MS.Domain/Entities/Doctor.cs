
using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities
{
    public class Doctor : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public string DisplayName { get; set; } // For UI display
        public string FullName { get; set; }
        public string? BioVi { get; set; }
        public string? BioEn { get; set; }
        public string? AcademicTitleVi { get; set; }
        public string? AcademicTitleEn { get; set; }
        public string? AvatarUrl { get; set; } // UI avatar
        public string? PhotoUrl { get; set; } // For backward compatibility

        public double AverageRating { get; set; }
        public int RatingCount { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int YearsOfExperience { get; set; }

        /// <summary>
        /// Deposit amount required to book an appointment with this doctor.
        /// This value is copied to each appointment at booking time.
        /// </summary>
        public decimal? BookingDepositAmount { get; set; }

        public ICollection<DoctorFacility> Facilities { get; set; } = new List<DoctorFacility>();
        public ICollection<DoctorAvailability> Availabilities { get; set; } = new List<DoctorAvailability>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<DoctorLanguage> Languages { get; set; } = new List<DoctorLanguage>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
