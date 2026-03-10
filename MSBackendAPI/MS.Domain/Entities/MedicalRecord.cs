using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a medical record created during a patient appointment
    /// </summary>
    public class MedicalRecord : EntityAuditBase<Guid>, IUserTracking, IEntityBase<Guid>
    {
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

        public string Symptoms { get; set; }      
        public string Diagnosis { get; set; }    
        public string? Notes { get; set; }       

        // Tracking fields
        public string CreateBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
