using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetValidPasswordResetToken
{
    /// <summary>
    /// Repository interface for retrieving a valid, unused, non-expired password reset token.
    /// </summary>
    public interface IGetValidPasswordResetToken
    {
        /// <summary>
        /// Retrieves a password reset token matching all validity conditions:
        /// correct user, matching token string, not used, and not expired.
        /// </summary>
        /// <param name="userId">The identifier of the user associated with the token.</param>
        /// <param name="token">The raw token string to match.</param>
        /// <returns>The matching <see cref="PasswordResetToken"/> if found and valid; otherwise null.</returns>
        Task<PasswordResetToken?> Execute(Guid userId, string token);
    }
}
