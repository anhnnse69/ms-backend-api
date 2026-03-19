using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetValidPasswordResetToken
{
    /// <summary>
    /// Provides read-only data access for retrieving a valid password reset token.
    /// </summary>
    public class GetValidPasswordResetTokenImpl : RepositoryQueryBase<PasswordResetToken, Guid, AppDbContext>, IGetValidPasswordResetToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetValidPasswordResetTokenImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetValidPasswordResetTokenImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a password reset token matching all validity conditions:
        /// correct user, matching token string, not used, and not expired.
        /// </summary>
        /// <param name="userId">The identifier of the user associated with the token.</param>
        /// <param name="token">The raw token string to match.</param>
        /// <returns>The matching <see cref="PasswordResetToken"/> if found and valid; otherwise null.</returns>
        public async Task<PasswordResetToken?> Execute(Guid userId, string token)
        {
            return await FindByCondition(
                    t => t.UserId == userId
                      && t.Token == token
                      && !t.IsUsed
                      && t.ExpiresAt >= DateTimeOffset.UtcNow,
                    false)
                .FirstOrDefaultAsync();
        }
    }
}
