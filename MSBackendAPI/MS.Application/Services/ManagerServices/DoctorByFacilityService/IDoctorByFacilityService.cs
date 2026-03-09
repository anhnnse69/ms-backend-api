using MS.Application.Common.Response;

namespace MS.Application.Services.DoctorsByFacilityService
{
    /// <summary>
    /// Interface for retrieving doctors by facility
    /// </summary>
    public interface IDoctorByFacilityService
    {
        /// <summary>
        /// Process request to retrieve doctors by facility with pagination
        /// </summary>
        /// <param name="facilityId">Facility identifier used to filter doctors</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Paginated list of doctors</returns>
        Task<ApiResponse<List<DoctorListResponse>>> Process(Guid facilityId, int page, int size);
    }
}