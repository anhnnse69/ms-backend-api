using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Types;

namespace MS.Domain.Entities
{
    public class Facility : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        public string NameVi { get; set; }
        public string NameEn { get; set; }
        public string DescriptionVi { get; set; } // UI
        public string DescriptionEn { get; set; } // UI
        public string? LogoUrl { get; set; } // UI logo
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public FacilityType Type { get; set; }

        public ICollection<DoctorFacility> DoctorFacilities { get; set; } = new List<DoctorFacility>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<FacilitySpecialty> Specialties { get; set; } = new List<FacilitySpecialty>();
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