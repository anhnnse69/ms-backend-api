using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetFavoriteById
{
    /// <summary>
    /// Repository implementation for retrieving a favorite record by its identifier.
    /// </summary>
    public class GetFavoriteByIdImpl : RepositoryQueryBase<Favorite, Guid, AppDbContext>, IGetFavoriteById
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFavoriteByIdImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetFavoriteByIdImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the favorite entity matching the specified identifier (active only).
        /// </summary>
        /// <param name="favoriteId">The unique identifier of the favorite record.</param>
        /// <returns>Favorite entity if found and not deleted; otherwise null.</returns>
        public async Task<Favorite?> Execute(Guid favoriteId)
        {
            return await FindByCondition(f => f.Id == favoriteId, false)
                .FirstOrDefaultAsync();
        }
    }
}
