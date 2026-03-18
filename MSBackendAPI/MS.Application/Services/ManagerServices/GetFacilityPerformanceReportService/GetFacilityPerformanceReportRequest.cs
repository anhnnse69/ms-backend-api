using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;

namespace MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService
{
    /// <summary>
    /// Represents the request to get facility performance report.
    /// </summary>
    public class GetFacilityPerformanceReportRequest
    {
        /// <summary>The start date of the reporting period (optional).</summary>
        public DateTime? StartDate { get; set; }

        /// <summary>The end date of the reporting period (optional).</summary>
        public DateTime? EndDate { get; set; }

        /// <summary>The specific doctor ID to filter by (optional).</summary>
        public Guid? DoctorId { get; set; }

        /// <summary>The facility ID for which the report is generated. This field is required.</summary>
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4003))]
        public Guid FacilityId { get; set; }
    }
}