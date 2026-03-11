using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetUserById
{
    /// <summary>
    /// Provides an implementation of the IGetUserById interface for retrieving
    /// user information by user identifier from the application's data store.
    /// </summary>
    public class GetUserByIdImpl : RepositoryQueryBase<User, Guid, AppDbContext>, IGetUserById
    {
        /// <summary>
        /// Initializes a new instance of the GetUserByIdImpl class using the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used to access user data. Cannot be null.
        /// </param>
        public GetUserByIdImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Processes the logic to retrieve a user by the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>
        /// A <see cref="User"/> entity if the user exists; otherwise, <c>null</c>.
        /// </returns>
        public async Task<User> Execute(Guid id)
        {
            return await FindByCondition(x => x.Id == id, false)
                .FirstOrDefaultAsync();
        }
    }
}