using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.SearchDoctorService
{
    /// <summary>
    /// Defines the contract for the search doctor service.
    /// </summary>
    public interface ISearchDoctorService
    {
        /// <summary>
        /// Processes the request to search doctors by keyword, specialty, facility, and location.
        /// </summary>
        /// <param name="keyword">Optional keyword to search by doctor name or facility name.</param>
        /// <param name="specialtyId">Optional specialty identifier to filter results.</param>
        /// <param name="facilityId">Optional facility identifier to filter results.</param>
        /// <param name="location">Optional city or address to filter by location.</param>
        /// <param name="page">Current page index.</param>
        /// <param name="size">Number of records per page.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a paginated list of doctor search results.
        /// </returns>
        Task<ApiResponse<List<SearchDoctorResponse>>> Process(
            string? keyword,
            Guid? specialtyId,
            Guid? facilityId,
            string? location,
            int page,
            int size);
    }
}
