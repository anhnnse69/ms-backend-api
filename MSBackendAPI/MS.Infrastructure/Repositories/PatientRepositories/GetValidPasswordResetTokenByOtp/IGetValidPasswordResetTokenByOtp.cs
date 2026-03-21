using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetValidPasswordResetTokenByOtp
{
    /// <summary>
    /// Repository interface for retrieving a valid, unused, non-expired OTP token by its code alone.
    /// </summary>
    public interface IGetValidPasswordResetTokenByOtp
    {
        /// <summary>
        /// Retrieves a valid OTP token matching the specified code,
        /// including the associated user entity.
        /// </summary>
        /// <param name="otpCode">The raw 6-digit OTP string to match.</param>
        /// <returns>
        /// The matching <see cref="PasswordResetToken"/> with <see cref="PasswordResetToken.User"/>
        /// populated if found and valid; otherwise null.
        /// </returns>
        Task<PasswordResetToken?> Execute(string otpCode);
    }
}
