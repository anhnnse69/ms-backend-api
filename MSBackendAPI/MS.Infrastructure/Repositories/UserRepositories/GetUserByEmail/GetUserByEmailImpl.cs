using MS.Domain.Entities;
using MS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MS.Infrastructure.Repositories.UserRepositories.GetUserByEmail
{
    /// <summary>
    /// Provides an implementation of the IGetUserByEmail interface for retrieving user information by email address
    /// from the application's data store.
    /// </summary>
    public class GetUserByEmailImpl : IGetUserByEmail
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the GetUserByEmailImpl class using the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used for accessing user data. Cannot be null.</param>
        public GetUserByEmailImpl(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Proccess logic for retrieve data
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public async Task<User> Execute(string emailAddress)
        {
            return await RetrieveData(emailAddress);
        }

        /// <summary>
        /// Asynchronously retrieves a user with the specified email address from the data store.
        /// </summary>
        /// <param name="emailAddress">The email address of the user to retrieve. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the user with the specified
        /// email address, or null if no such user exists.</returns>
        private async Task<User> RetrieveData(string emailAddress)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == emailAddress);
        }
    }
}
