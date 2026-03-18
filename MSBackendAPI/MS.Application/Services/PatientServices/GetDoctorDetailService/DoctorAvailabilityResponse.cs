namespace MS.Application.Services.PatientServices.GetDoctorDetailService
{
    /// <summary>
    /// Represents a weekly availability schedule entry for a doctor.
    /// </summary>
    public class DoctorAvailabilityResponse
    {
        /// <summary>The day of the week for this availability entry.</summary>
        public int DayOfWeek { get; set; }
        /// <summary>The start time of the availability slot.</summary>
        public TimeSpan StartTime { get; set; }
        /// <summary>The end time of the availability slot.</summary>
        public TimeSpan EndTime { get; set; }
        /// <summary>The duration in minutes of each appointment slot.</summary>
        public int SlotDurationMinutes { get; set; }
    }
}
