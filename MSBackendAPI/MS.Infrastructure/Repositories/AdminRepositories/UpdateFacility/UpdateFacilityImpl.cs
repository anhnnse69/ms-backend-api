using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Common.Contracts.Interfaces;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.UpdateFacility
{
    /// <summary>
    /// Provides an implementation for updating facility information in the database.
    /// </summary>
    public class UpdateFacilityImpl : RepositoryBase<Facility, Guid, AppDbContext>, IUpdateFacility
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateFacilityImpl"/> class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access the facility data.
        /// </param>
        /// <param name="unitOfWork">
        /// The unit of work responsible for managing database transactions.
        /// </param>
        public UpdateFacilityImpl( AppDbContext context, IUnitOfWork<AppDbContext> unitOfWork) : base(context, unitOfWork)
        {
        }

        /// <summary>
        /// Updates the specified facility entity and persists the changes to the database.
        /// </summary>
        /// <param name="facility">
        /// The facility entity containing updated data that needs to be saved.
        /// </param>
        /// <returns>
        /// A task representing the asynchronous update operation.
        /// </returns>
        public async Task Execute(Facility facility)
        {
            await UpdateAsync(facility);
            await SaveChangesAsync();
        }
    }
}