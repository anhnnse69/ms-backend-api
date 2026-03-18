using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.SearchDoctor
{
    /// <summary>
    /// Repository interface for searching doctors with dynamic filters and pagination.
    /// </summary>
    public interface ISearchDoctor
    {
        /// <summary>
        /// Retrieves a paginated list of doctors matching the specified search criteria.
        /// </summary>
        /// <param name="keyword">Optional keyword to filter by doctor full name or facility name.</param>
        /// <param name="specialtyId">Optional specialty identifier to filter doctors by specialty.</param>
        /// <param name="facilityId">Optional facility identifier to filter doctors by facility.</param>
        /// <param name="location">Optional city or address string to filter by location.</param>
        /// <param name="page">The current page index (1-based).</param>
        /// <param name="size">The number of records per page.</param>
        /// <returns>
        /// A tuple containing the list of matching doctor entities and the total record count.
        /// </returns>
        Task<(List<Doctor> doctors, int total)> Execute(
            string? keyword,
            Guid? specialtyId,
            Guid? facilityId,
            string? location,
            int page,
            int size);
    }
}
