using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdatePasswordHash
{
    /// <summary>
    /// Provides data access for updating a user's password hash.
    /// </summary>
    public class UpdatePasswordHashImpl
        : RepositoryBase<User, Guid, AppDbContext>, IUpdatePasswordHash
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePasswordHashImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public UpdatePasswordHashImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Updates the password hash for the specified user and persists the change.
        /// </summary>
        /// <param name="user">The user entity whose password hash will be updated.</param>
        /// <param name="passwordHash">The new BCrypt password hash to store.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(User user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            user.LastModifiedBy = user.Id.ToString();
            await UpdateAsync(user);
            await SaveChangesAsync();
        }
    }
}
