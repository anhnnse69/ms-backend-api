using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.CreateUser
{
    /// <summary>
    /// Provides an implementation of the ICreateUser interface
    /// for inserting a new user into the database.
    /// </summary>
    public class CreateUserImpl : RepositoryBase<User, Guid, AppDbContext>, ICreateUser
    {
        /// <summary>
        /// Initializes a new instance of the CreateUserImpl class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access user data.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work responsible for managing database transactions.
        /// </param>
        public CreateUserImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork) { }

        /// <summary>
        /// Inserts a new user record into the database.
        /// </summary>
        /// <param name="user">
        /// The <see cref="User"/> entity to be created.
        /// </param>
        /// <returns>
        /// The created <see cref="User"/> entity.
        /// </returns>
        public async Task<User> Execute(User user)
        {
            await CreateAsync(user);
            await SaveChangesAsync();
            return user;
        }
    }
}