using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents doctor's working schedule
    /// </summary>
    public class DoctorAvailability
    : EntityAuditBase<Guid>, IEntityBase<Guid>, IUserTracking
    {
        [Key]
        public Guid Id { get; set; }

        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public Guid FacilityId { get; set; }
        public Facility Facility { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDurationMinutes { get; set; }
        public bool IsActive { get; set; }

        // Tracking fields for auditing
        public string CreateBy { get; set; }
        public string LastModifiedBy { get; set; }
    }
}
