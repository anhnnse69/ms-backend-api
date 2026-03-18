namespace MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService
{
    /// <summary>
    /// Represents the performance metrics for a specific doctor.
    /// </summary>
    public class DoctorPerformanceResponse
    {
        /// <summary>The unique identifier of the doctor.</summary>
        public Guid DoctorId { get; set; }

        /// <summary>The full name of the doctor.</summary>
        public string DoctorName { get; set; }

        /// <summary>The total number of appointments assigned to this doctor.</summary>
        public int TotalAppointments { get; set; }

        /// <summary>The number of completed appointments for this doctor.</summary>
        public int CompletedAppointments { get; set; }

        /// <summary>The number of cancelled appointments for this doctor.</summary>
        public int CancelledAppointments { get; set; }

        /// <summary>The completion rate percentage for this doctor.</summary>
        public double CompletionRate { get; set; }
    }
}
