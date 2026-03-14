using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.SpecialtyRepositories.GetSpecialtyByName
{
    /// <summary>
    /// Defines the contract for retrieving a specialty by Vietnamese name.
    /// </summary>
    public interface IGetSpecialtyByName
    {
        /// <summary>
        /// Retrieves a specialty by Vietnamese name.
        /// </summary>
        /// <param name="name">Vietnamese name of specialty.</param>
        /// <returns>The specialty if found.</returns>
        Task<Specialty?> Execute(string name);
    }
}