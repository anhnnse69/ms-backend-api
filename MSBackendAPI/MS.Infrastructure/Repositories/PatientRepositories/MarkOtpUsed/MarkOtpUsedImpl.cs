using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.MarkOtpUsed
{
    /// <summary>
    /// Provides data access for marking a password reset OTP token as used.
    /// </summary>
    public class MarkOtpUsedImpl
        : RepositoryBase<PasswordResetToken, Guid, AppDbContext>, IMarkOtpUsed
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkOtpUsedImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public MarkOtpUsedImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Marks the specified OTP token as used and records the usage timestamp.
        /// </summary>
        /// <param name="token">The <see cref="PasswordResetToken"/> to mark as used.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Execute(PasswordResetToken token)
        {
            token.IsUsed = true;
            token.UsedAt = DateTimeOffset.UtcNow;
            await UpdateAsync(token);
            await SaveChangesAsync();
        }
    }
}
