using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllUsers
{
    /// <summary>
    /// Provides an implementation of the IGetAllUsers interface for retrieving 
    /// all user records from the application's data store.
    /// </summary>
    public class GetAllUsersImpl : RepositoryQueryBase<User, Guid, AppDbContext>, IGetAllUsers
    {
        /// <summary>
        /// Initializes a new instance of the GetAllUsersImpl class using the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used for accessing user data. Cannot be null.
        /// </param>
        public GetAllUsersImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Processes the logic to retrieve all users from the database.
        /// </summary>
        /// <returns>
        /// A list of <see cref="User"/> entities representing all users in the system.
        /// </returns>
        public IQueryable<User> Execute()
        {
            return FindAll(trackChanges: false);
        }
    }
}