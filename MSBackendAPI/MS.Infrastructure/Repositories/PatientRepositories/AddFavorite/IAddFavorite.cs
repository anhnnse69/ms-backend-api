using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.AddFavorite
{
    /// <summary>
    /// Repository interface for persisting a new favorite record.
    /// </summary>
    public interface IAddFavorite
    {
        /// <summary>
        /// Persists the specified favorite entity to the database.
        /// </summary>
        /// <param name="favorite">The favorite entity to persist.</param>
        /// <returns>The persisted favorite entity.</returns>
        Task<Favorite> Execute(Favorite favorite);
    }
}
