using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.RestoreFavorite
{
    /// <summary>
    /// Repository interface for restoring a soft-deleted favorite record.
    /// </summary>
    public interface IRestoreFavorite
    {
        /// <summary>
        /// Restores the specified soft-deleted favorite by clearing deletion fields.
        /// </summary>
        /// <param name="favorite">The soft-deleted favorite entity to restore.</param>
        /// <param name="modifiedBy">The identifier of the user performing the restore.</param>
        /// <returns>The restored favorite entity.</returns>
        Task<Favorite> Execute(Favorite favorite, string modifiedBy);
    }
}
