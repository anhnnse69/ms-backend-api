using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.SoftDeleteFavorite
{
    /// <summary>
    /// Repository implementation for performing a soft delete on a favorite record.
    /// </summary>
    public class SoftDeleteFavoriteImpl : RepositoryBase<Favorite, Guid, AppDbContext>, ISoftDeleteFavorite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SoftDeleteFavoriteImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public SoftDeleteFavoriteImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Marks the specified favorite as deleted and persists the change.
        /// </summary>
        /// <param name="favorite">The favorite entity to soft delete.</param>
        /// <param name="deletedBy">The identifier of the user performing the deletion.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(Favorite favorite, string deletedBy)
        {
            favorite.IsDeleted = true;
            favorite.DeletedAt = DateTimeOffset.UtcNow;
            favorite.DeletedBy = deletedBy;
            favorite.LastModifiedBy = deletedBy;
            favorite.LastModifiedDate = DateTimeOffset.UtcNow;
            await UpdateAsync(favorite);
            await SaveChangesAsync();
        }
    }
}
