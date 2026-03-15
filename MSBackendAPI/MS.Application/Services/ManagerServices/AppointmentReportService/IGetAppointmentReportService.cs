using MS.Application.Common.Response;

namespace MS.Application.Services.ManagerServices.AppointmentReportService
{
    /// <summary>
    /// Service interface for generating appointment reports for a facility.
    /// </summary>
    public interface IGetAppointmentReportService
    {
        /// <summary>
        /// Processes the appointment report request and returns statistics of appointments
        /// based on the specified facility and report type (day, month, or year).
        /// </summary>
        /// <param name="request">
        /// The request containing facility identifier, report type, and optional date filter.
        /// </param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing <see cref="AppointmentReportResponse"/>
        /// with appointment status statistics.
        /// </returns>
        Task<ApiResponse<AppointmentReportResponse>> Process(AppointmentReportRequest request);
    }
}