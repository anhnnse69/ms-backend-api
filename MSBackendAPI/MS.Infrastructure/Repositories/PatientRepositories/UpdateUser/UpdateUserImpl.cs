using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdateUser
{
    /// <summary>
    /// Provides an implementation of the IUpdateUser interface
    /// for updating existing user records in the database.
    /// </summary>
    public class UpdateUserImpl : RepositoryBase<User, Guid, AppDbContext>, IUpdateUser
    {
        /// <summary>
        /// Initializes a new instance of the UpdateUserImpl class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access user data.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work responsible for managing database transactions.
        /// </param>
        public UpdateUserImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork) { }

        /// <summary>
        /// Updates an existing user record in the database.
        /// </summary>
        /// <param name="user">
        /// The <see cref="User"/> entity containing updated user information.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous update operation.
        /// </returns>
        public async Task Execute(User user)
        {
            await UpdateAsync(user);
            await SaveChangesAsync();
        }
    }
}