using MS.Domain.Enums.Types;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a medical appointment
    /// </summary>
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }

        public Guid SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }

        public Guid? DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public DateTime AppointmentTime { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public AppointmentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public string CancellationReason { get; set; }
    }
}
