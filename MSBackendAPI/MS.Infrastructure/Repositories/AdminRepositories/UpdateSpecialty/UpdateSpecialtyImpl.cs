using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.UpdateSpecialty
{
    /// <summary>
    /// Provides an implementation for updating specialty information in the database.
    /// </summary>
    public class UpdateSpecialtyImpl : RepositoryBase<Specialty, Guid, AppDbContext>, IUpdateSpecialty
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSpecialtyImpl"/> class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access specialty data.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work responsible for managing database transactions.
        /// </param>
        public UpdateSpecialtyImpl(
            AppDbContext context,
            IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Updates the specified specialty entity and persists the changes to the database.
        /// </summary>
        /// <param name="specialty">
        /// The specialty entity containing updated information.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous update operation.
        /// </returns>
        public async Task Execute(Specialty specialty)
        {
            await UpdateAsync(specialty);
            await SaveChangesAsync();
        }
    }
}