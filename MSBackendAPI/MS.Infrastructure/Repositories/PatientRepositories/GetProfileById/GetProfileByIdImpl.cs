using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetProfileById
{
    /// <summary>
    /// Repository implementation for retrieving a user by their unique identifier.
    /// </summary>
    public class GetProfileByIdImpl : RepositoryQueryBase<User, Guid, AppDbContext>, IGetProfileById
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProfileByIdImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetProfileByIdImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the user matching the specified identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns><see cref="User"/> if found; otherwise null.</returns>
        public async Task<User?> Execute(Guid userId)
        {
            return await FindByCondition(u => u.Id == userId, false)
                .FirstOrDefaultAsync();
        }
    }
}
