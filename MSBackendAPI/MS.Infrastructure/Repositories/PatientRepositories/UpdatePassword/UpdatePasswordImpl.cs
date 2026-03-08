using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.UpdatePassword
{
    /// <summary>
    /// Implementation for updating user password
    /// </summary>
    public class UpdatePasswordImpl
        : RepositoryBase<User, Guid, AppDbContext>, IUpdatePassword
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public UpdatePasswordImpl(
            AppDbContext context,
            IUnitOfWork<AppDbContext> unitOfWork)
            : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Execute update password
        /// </summary>
        public async Task Execute(User user)
        {
            await UpdateAsync(user);
            await SaveChangesAsync();
        }
    }
}