namespace MS.Application.Services.ManagerServices.DoctorScheduleByFacilityService
{
    public class ScheduleSlotResponse
    {
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SlotDurationMinutes { get; set; }
    }
}
