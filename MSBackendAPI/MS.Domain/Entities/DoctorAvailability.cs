using System.ComponentModel.DataAnnotations;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents doctor's working schedule
    /// </summary>
    public class DoctorAvailability
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
    }
}
