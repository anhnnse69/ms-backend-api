using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetPatientFavorites
{
    /// <summary>
    /// Repository interface for retrieving a patient's favorites list with filtering and pagination.
    /// </summary>
    public interface IGetPatientFavorites
    {
        /// <summary>
        /// Retrieves a paginated list of active favorites for the specified patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="type">Filter type: "Doctor", "Facility", or "All".</param>
        /// <param name="page">The current page index (1-based).</param>
        /// <param name="size">The number of records per page.</param>
        /// <returns>Tuple of favorites list and total count matching the filter.</returns>
        Task<(List<Favorite> favorites, int total)> Execute(Guid patientId, string type, int page, int size);
    }
}
