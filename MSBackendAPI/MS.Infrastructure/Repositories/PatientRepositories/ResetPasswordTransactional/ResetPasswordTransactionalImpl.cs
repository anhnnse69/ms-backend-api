using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.ResetPasswordTransactional
{
    /// <summary>
    /// Provides transactional data access for resetting a user password and invalidating the reset token atomically.
    /// </summary>
    public class ResetPasswordTransactionalImpl : RepositoryBase<User, Guid, AppDbContext>, IResetPasswordTransactional
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordTransactionalImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public ResetPasswordTransactionalImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork)
        {
            _context = context;
        }

        /// <summary>
        /// Updates the user's password hash and marks the reset token as used within a single database transaction.
        /// Rolls back all changes if any operation fails.
        /// </summary>
        /// <param name="user">The user entity whose password will be updated.</param>
        /// <param name="passwordHash">The new hashed password to store.</param>
        /// <param name="resetToken">The password reset token to mark as used.</param>
        /// <returns>A task representing the asynchronous transactional operation.</returns>
        public async Task Execute(User user, string passwordHash, PasswordResetToken resetToken)
        {
            await using var transaction = await BeginTransactionAsync();
            try
            {
                user.PasswordHash = passwordHash;
                user.LastModifiedBy = user.Id.ToString();
                await UpdateAsync(user);
                resetToken.IsUsed = true;
                resetToken.UsedAt = DateTimeOffset.UtcNow;
                _context.Set<PasswordResetToken>().Update(resetToken);
                await EndTransactionAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
        }
    }
}
