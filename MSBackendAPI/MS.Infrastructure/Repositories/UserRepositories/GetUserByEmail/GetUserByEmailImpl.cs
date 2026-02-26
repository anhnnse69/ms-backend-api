using MS.Domain.Entities;
using MS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MS.Infrastructure.Common.Contracts;

namespace MS.Infrastructure.Repositories.UserRepositories.GetUserByEmail
{
    /// <summary>
    /// Provides an implementation of the IGetUserByEmail interface for retrieving user information by email address
    /// from the application's data store.
    /// </summary>
    public class GetUserByEmailImpl : RepositoryQueryBase<User, Guid, AppDbContext>, IGetUserByEmail
    {
        /// <summary>
        /// Initializes a new instance of the GetUserByEmailImpl class using the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used for accessing user data. Cannot be null.</param>
        public GetUserByEmailImpl(AppDbContext context) : base(context)
        { }

        /// <summary>
        /// Proccess logic for retrieve data
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<User> Execute(string emailAddress)
        {
            return await FindByCondition(user => user.Email == emailAddress, trackChanges: false)
            .FirstOrDefaultAsync();
        }
    }
}
