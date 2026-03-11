using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities
{
    public class Specialty : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        public string NameVi { get; set; }
        public string NameEn { get; set; }
        public string DescriptionVi { get; set; }
        public string DescriptionEn { get; set; }
        public string? IconUrl { get; set; } // UI icon

        public int DisplayOrder { get; set; }

        public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<FacilitySpecialty> Facilities { get; set; } = new List<FacilitySpecialty>();

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
