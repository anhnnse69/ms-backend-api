using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.AddFavorite
{
    /// <summary>
    /// Repository implementation for persisting a new favorite record.
    /// </summary>
    public class AddFavoriteImpl : RepositoryBase<Favorite, Guid, AppDbContext>, IAddFavorite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddFavoriteImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public AddFavoriteImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Persists the specified favorite entity to the database.
        /// </summary>
        /// <param name="favorite">The favorite entity to persist.</param>
        /// <returns>The persisted favorite entity.</returns>
        public async Task<Favorite> Execute(Favorite favorite)
        {
            await CreateAsync(favorite);
            await SaveChangesAsync();
            return favorite;
        }
    }
}
