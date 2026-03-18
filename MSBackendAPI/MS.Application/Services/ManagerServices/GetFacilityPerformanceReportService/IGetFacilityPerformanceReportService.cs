using MS.Application.Common.Response;

namespace MS.Application.Services.ManagerServices.GetFacilityPerformanceReportService
{
    /// <summary>
    /// Interface for handling facility performance report requests.
    /// </summary>
    public interface IGetFacilityPerformanceReportService
    {
        /// <summary>
        /// Processes the request to retrieve facility performance report.
        /// </summary>
        /// <param name="request">The request containing filter criteria (facility, date range, optional doctor).</param>
        /// <param name="managerId">The unique identifier of the manager making the request.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing <see cref="GetFacilityPerformanceReportResponse"/>
        /// with overall and per-doctor performance metrics.
        /// </returns>
        Task<ApiResponse<GetFacilityPerformanceReportResponse>> Process(GetFacilityPerformanceReportRequest request, Guid managerId);
    }
}