using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.RestoreFavorite
{
    /// <summary>
    /// Repository implementation for restoring a soft-deleted favorite record.
    /// </summary>
    public class RestoreFavoriteImpl : RepositoryBase<Favorite, Guid, AppDbContext>, IRestoreFavorite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RestoreFavoriteImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public RestoreFavoriteImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Restores the specified soft-deleted favorite by clearing deletion fields.
        /// </summary>
        /// <param name="favorite">The soft-deleted favorite entity to restore.</param>
        /// <param name="modifiedBy">The identifier of the user performing the restore.</param>
        /// <returns>The restored favorite entity.</returns>
        public async Task<Favorite> Execute(Favorite favorite, string modifiedBy)
        {
            favorite.IsDeleted = false;
            favorite.DeletedAt = null;
            favorite.DeletedBy = null;
            favorite.LastModifiedBy = modifiedBy;
            favorite.LastModifiedDate = DateTimeOffset.UtcNow;
            await UpdateAsync(favorite);
            await SaveChangesAsync();
            return favorite;
        }
    }
}
