using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.MarkOtpUsed
{
    /// <summary>
    /// Repository interface for marking a password reset OTP token as used.
    /// </summary>
    public interface IMarkOtpUsed
    {
        /// <summary>
        /// Marks the specified OTP token as used and records the usage timestamp.
        /// </summary>
        /// <param name="token">The <see cref="PasswordResetToken"/> to mark as used.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Execute(PasswordResetToken token);
    }
}
