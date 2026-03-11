using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using MS.Domain.Enums.Types;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a medical appointment
    /// </summary>
    public class Appointment : EntityAuditBase<Guid>, IUserTracking, ISoftDeletable, IEntityBase<Guid>
    {
        public Guid PatientId { get; set; }
        public Patient? Patient { get; set; }

        public Guid FacilityId { get; set; }
        public Facility? Facility { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }

        public Guid? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public DateTimeOffset AppointmentTime { get; set; }
        public string? Notes { get; set; }
        public AppointmentStatus Status { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
        public DateTimeOffset? CancelledAt { get; set; }
        public string? CancellationReason { get; set; }

        // Soft delete fields
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
