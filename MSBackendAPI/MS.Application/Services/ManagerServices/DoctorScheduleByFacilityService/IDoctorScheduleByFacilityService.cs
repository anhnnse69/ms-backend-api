using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorScheduleService
{
    /// <summary>
    /// Interface for retrieving doctor schedules by facility
    /// </summary>
    public interface IDoctorScheduleService
    {
        /// <summary>
        /// Process request to retrieve doctor schedules for a specific facility
        /// </summary>
        /// <param name="facilityId">Facility identifier</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of doctor schedules</returns>
        Task<ApiResponse<List<DoctorScheduleResponse>>> Process(Guid facilityId, int page, int size);
    }
}