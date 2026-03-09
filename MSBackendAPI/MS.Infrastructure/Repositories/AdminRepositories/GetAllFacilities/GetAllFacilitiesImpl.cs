using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllFacilities
{
    /// <summary>
    /// Provides an implementation of the IGetAllFacilities interface for retrieving
    /// all facility records from the application's data store.
    /// </summary>
    public class GetAllFacilitiesImpl
        : RepositoryQueryBase<Facility, Guid, AppDbContext>, IGetAllFacilities
    {
        /// <summary>
        /// Initializes a new instance of the GetAllFacilitiesImpl class
        /// using the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used for accessing facility data.
        /// </param>
        public GetAllFacilitiesImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Processes the logic to retrieve all facilities from the database.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{Facility}"/> representing all facilities.
        /// </returns>
        public IQueryable<Facility> Execute()
        {
            return FindAll(trackChanges: false);
        }
    }
}