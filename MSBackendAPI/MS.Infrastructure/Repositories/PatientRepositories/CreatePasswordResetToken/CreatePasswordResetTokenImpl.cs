using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.CreatePasswordResetToken
{
    /// <summary>
    /// Provides data access for creating and persisting a password reset token.
    /// </summary>
    public class CreatePasswordResetTokenImpl : RepositoryBase<PasswordResetToken, Guid, AppDbContext>, ICreatePasswordResetToken
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePasswordResetTokenImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        /// <param name="unitOfWork">Unit of work instance.</param>
        public CreatePasswordResetTokenImpl(AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork) { }

        /// <summary>
        /// Persists the specified password reset token to the database.
        /// </summary>
        /// <param name="token">The <see cref="PasswordResetToken"/> entity to persist.</param>
        /// <returns>The persisted <see cref="PasswordResetToken"/> entity.</returns>
        public async Task<PasswordResetToken> Execute(PasswordResetToken token)
        {
            await CreateAsync(token);
            await SaveChangesAsync();
            return token;
        }
    }
}
