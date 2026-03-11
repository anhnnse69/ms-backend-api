using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a review/rating given by a patient to a doctor or facility
    /// </summary>
    public class Review : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        // Patient who made the review
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }

        // Link to the appointment (must be completed)
        public Guid AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }

        // Doctor being reviewed (optional - can review facility instead)
        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        // Facility being reviewed (optional - can review doctor instead)
        public Guid? FacilityId { get; set; }
        public Facility? Facility { get; set; }

        // Rating (1-5 stars)
        public int Rating { get; set; }

        // Review comment/text
        public string? Comment { get; set; }

        // Visibility control
        public bool IsVisible { get; set; } = true;

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
