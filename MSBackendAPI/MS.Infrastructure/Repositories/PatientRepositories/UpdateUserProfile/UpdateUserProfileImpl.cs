using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdateUserProfile
{
    /// <summary>
    /// Repository implementation for persisting user profile updates to the database.
    /// </summary>
    public class UpdateUserProfileImpl : RepositoryBase<User, Guid, AppDbContext>, IUpdateUserProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserProfileImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance for committing changes.</param>
        public UpdateUserProfileImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Persists the updated user entity to the database.
        /// </summary>
        /// <param name="user">The user entity containing updated profile data.</param>
        /// <returns>A task representing the asynchronous save operation.</returns>
        public async Task Execute(User user)
        {
            await UpdateAsync(user);
            await SaveChangesAsync();
        }
    }
}
