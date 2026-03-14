using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllSpecialties
{
    /// <summary>
    /// Repository interface used to retrieve all specialties from database.
    /// </summary>
    public interface IGetAllSpecialties
    {
        /// <summary>
        /// Executes the query to get all specialties.
        /// </summary>
        /// <returns>
        /// IQueryable list of specialties.
        /// </returns>
        IQueryable<Specialty> Execute();
    }
}