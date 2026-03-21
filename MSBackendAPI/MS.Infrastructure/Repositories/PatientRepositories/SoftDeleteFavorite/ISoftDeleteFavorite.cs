using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.SoftDeleteFavorite
{
    /// <summary>
    /// Repository interface for performing a soft delete on a favorite record.
    /// </summary>
    public interface ISoftDeleteFavorite
    {
        /// <summary>
        /// Marks the specified favorite as deleted and persists the change.
        /// </summary>
        /// <param name="favorite">The favorite entity to soft delete.</param>
        /// <param name="deletedBy">The identifier of the user performing the deletion.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Execute(Favorite favorite, string deletedBy);
    }
}
