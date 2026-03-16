using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.GetAllDoctors
{
    /// <summary>
    /// Defines a repository contract for retrieving all doctors from the data store.
    /// </summary>
    public interface IGetAllDoctors
    {
        /// <summary>
        /// Executes the query to retrieve all doctors.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{Doctor}"/> representing the doctor query.
        /// </returns>
        IQueryable<Doctor> Execute();
    }
}
