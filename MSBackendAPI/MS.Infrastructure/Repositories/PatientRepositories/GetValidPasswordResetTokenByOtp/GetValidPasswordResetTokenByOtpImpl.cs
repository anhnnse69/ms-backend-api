using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetValidPasswordResetTokenByOtp
{
    /// <summary>
    /// Provides read-only data access for retrieving a valid OTP token by its code,
    /// including the associated user via eager loading.
    /// </summary>
    public class GetValidPasswordResetTokenByOtpImpl
        : RepositoryQueryBase<PasswordResetToken, Guid, AppDbContext>,
          IGetValidPasswordResetTokenByOtp
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetValidPasswordResetTokenByOtpImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetValidPasswordResetTokenByOtpImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a valid OTP token matching the specified code,
        /// including the associated user entity.
        /// </summary>
        /// <param name="otpCode">The raw 6-digit OTP string to match.</param>
        /// <returns>
        /// The matching <see cref="PasswordResetToken"/> with <see cref="PasswordResetToken.User"/>
        /// populated if found and valid; otherwise null.
        /// </returns>
        public async Task<PasswordResetToken?> Execute(string otpCode)
        {
            return await FindByCondition(
                    t => t.Token == otpCode
                      && !t.IsUsed
                      && t.ExpiresAt >= DateTimeOffset.UtcNow,
                    false)
                .Include(t => t.User)
                .FirstOrDefaultAsync();
        }
    }
}
