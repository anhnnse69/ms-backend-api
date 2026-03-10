namespace MS.Application.Services.DoctorScheduleService
{
    /// <summary>
    /// Response model representing doctor schedule information
    /// </summary>
    public class DoctorScheduleResponse
    {
        public Guid DoctorId { get; set; }
        public string DoctorName { get; set; }
        public List<ScheduleSlot> Schedules { get; set; }
    }
    public class ScheduleSlot
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDurationMinutes { get; set; }
    }
}