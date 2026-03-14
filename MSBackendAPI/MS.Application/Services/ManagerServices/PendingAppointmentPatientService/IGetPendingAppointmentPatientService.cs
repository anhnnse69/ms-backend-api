using MS.Application.Common.Response;

namespace MS.Application.Services.ManagerServices.PendingAppointmentPatientService
{
    /// <summary>
    /// Service interface for retrieving pending patient appointments
    /// </summary>
    public interface IGetPendingAppointmentPatientService
    {
        /// <summary>
        /// Process request to retrieve pending patient appointments
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of pending appointment patients</returns>
        Task<ApiResponse<List<PendingAppointmentResponse>>> Process(int page, int size);
    }
}