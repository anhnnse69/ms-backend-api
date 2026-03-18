namespace MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService
{
    /// <summary>
    /// Request model for retrieving facility performance report.
    /// </summary>
    public class GetFacilityPerformanceReportRequest
    {
        /// <summary>Optional facility identifier for cross-validation; if omitted, the manager's assigned facility is used.</summary>
        public Guid? FacilityId { get; set; }
        /// <summary>Optional start date for filtering appointments (inclusive).</summary>
        public DateTime? StartDate { get; set; }
        /// <summary>Optional end date for filtering appointments (inclusive).</summary>
        public DateTime? EndDate { get; set; }
        /// <summary>Optional doctor identifier to filter results for a specific doctor.</summary>
        public Guid? DoctorId { get; set; }
    }
}