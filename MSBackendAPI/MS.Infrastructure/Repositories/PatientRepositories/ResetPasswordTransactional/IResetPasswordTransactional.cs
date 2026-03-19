using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.ResetPasswordTransactional
{
    /// <summary>
    /// Repository interface for executing password reset and token invalidation atomically within a transaction.
    /// </summary>
    public interface IResetPasswordTransactional
    {
        /// <summary>
        /// Updates the user's password hash and marks the reset token as used within a single database transaction.
        /// Rolls back all changes if any operation fails.
        /// </summary>
        /// <param name="user">The user entity whose password will be updated.</param>
        /// <param name="passwordHash">The new hashed password to store.</param>
        /// <param name="resetToken">The password reset token to mark as used.</param>
        /// <returns>A task representing the asynchronous transactional operation.</returns>
        Task Execute(User user, string passwordHash, PasswordResetToken resetToken);
    }
}
