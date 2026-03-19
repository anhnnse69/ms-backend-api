using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.CreatePasswordResetToken
{
    /// <summary>
    /// Repository interface for persisting a new password reset token.
    /// </summary>
    public interface ICreatePasswordResetToken
    {
        /// <summary>
        /// Persists the specified password reset token to the database.
        /// </summary>
        /// <param name="token">The <see cref="PasswordResetToken"/> entity to persist.</param>
        /// <returns>The persisted <see cref="PasswordResetToken"/> entity.</returns>
        Task<PasswordResetToken> Execute(PasswordResetToken token);
    }
}
