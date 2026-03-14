using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllSpecialties
{
    /// <summary>
    /// Provides implementation for retrieving all specialties
    /// from the application's database.
    /// </summary>
    public class GetAllSpecialtiesImpl
        : RepositoryQueryBase<Specialty, Guid, AppDbContext>, IGetAllSpecialties
    {
        /// <summary>
        /// Initializes a new instance of the GetAllSpecialtiesImpl class.
        /// </summary>
        /// <param name="context">
        /// The database context used to access specialty data.
        /// </param>
        public GetAllSpecialtiesImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Executes the logic to retrieve all specialties.
        /// </summary>
        /// <returns>
        /// An IQueryable list of specialties.
        /// </returns>
        public IQueryable<Specialty> Execute()
        {
            return FindAll(trackChanges: false);
        }
    }
}