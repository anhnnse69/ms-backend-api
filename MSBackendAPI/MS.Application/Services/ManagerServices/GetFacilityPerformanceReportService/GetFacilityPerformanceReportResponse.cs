namespace MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService
{
    /// <summary>
    /// Represents the facility performance report response.
    /// </summary>
    public class GetFacilityPerformanceReportResponse
    {
        /// <summary>The total number of appointments in the period.</summary>
        public int TotalAppointments { get; set; }

        /// <summary>The number of completed appointments.</summary>
        public int CompletedAppointments { get; set; }

        /// <summary>The number of cancelled appointments.</summary>
        public int CancelledAppointments { get; set; }

        /// <summary>The completion rate percentage (Completed / Total * 100).</summary>
        public double CompletionRate { get; set; }

        /// <summary>The list of performances grouped by individual doctors.</summary>
        public List<DoctorPerformanceResponse> DoctorPerformances { get; set; } = new List<DoctorPerformanceResponse>();
    }
}
