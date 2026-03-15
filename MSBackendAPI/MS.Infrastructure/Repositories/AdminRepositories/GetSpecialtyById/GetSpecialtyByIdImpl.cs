using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetSpecialtyById
{
    /// <summary>
    /// Provides an implementation for retrieving a specialty by its unique identifier.
    /// </summary>
    public class GetSpecialtyByIdImpl
        : RepositoryQueryBase<Specialty, Guid, AppDbContext>, IGetSpecialtyById
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSpecialtyByIdImpl"/> class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access specialty data.
        /// </param>
        public GetSpecialtyByIdImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves a specialty by its unique identifier.
        /// </summary>
        /// <param name="id">
        /// The unique identifier of the specialty.
        /// </param>
        /// <returns>
        /// The specialty entity if found and not deleted; otherwise, null.
        /// </returns>
        public async Task<Specialty?> Execute(Guid id)
        {
            return await FindByCondition(
                    x => x.Id == id && !x.IsDeleted,
                    false)
                .FirstOrDefaultAsync();
        }
    }
}