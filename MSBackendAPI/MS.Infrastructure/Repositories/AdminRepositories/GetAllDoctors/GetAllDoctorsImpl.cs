using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllDoctors
{
    /// <summary>
    /// Provides an implementation of the IGetAllDoctors interface for retrieving 
    /// all doctor records from the application's data store.
    /// </summary>
    public class GetAllDoctorsImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, IGetAllDoctors
    {
        /// <summary>
        /// Initializes a new instance of the GetAllDoctorsImpl class using the specified database context.
        /// </summary>
        /// <param name="context">
        /// The database context used for accessing doctor data. Cannot be null.
        /// </param>
        public GetAllDoctorsImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Processes the logic to retrieve all doctors from the database.
        /// </summary>
        /// <returns>
        /// A queryable collection of <see cref="Doctor"/> entities.
        /// </returns>
        public IQueryable<Doctor> Execute()
        {
            return FindAll(trackChanges: false);
        }
    }
}
