using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetFavoriteById
{
    /// <summary>
    /// Repository interface for retrieving a favorite record by its identifier.
    /// </summary>
    public interface IGetFavoriteById
    {
        /// <summary>
        /// Retrieves the favorite entity matching the specified identifier (active only).
        /// </summary>
        /// <param name="favoriteId">The unique identifier of the favorite record.</param>
        /// <returns>Favorite entity if found and not deleted; otherwise null.</returns>
        Task<Favorite?> Execute(Guid favoriteId);
    }
}
