using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a patient's favorite doctor or facility
    /// </summary>
    public class Favorite : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        // Patient who added the favorite
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }

        // Favorite doctor (optional - can favorite facility instead)
        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        // Favorite facility (optional - can favorite doctor instead)
        public Guid? FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
