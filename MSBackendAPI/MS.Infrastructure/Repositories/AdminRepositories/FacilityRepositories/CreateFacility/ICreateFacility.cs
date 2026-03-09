using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.AdminRepositories.FacilityRepositories.CreateFacility
{
    /// <summary>
    /// Defines the contract for creating a facility.
    /// </summary>
    public interface ICreateFacility
    {
        /// <summary>
        /// Inserts a new facility into the database.
        /// </summary>
        /// <param name="facility">
        /// The facility entity to be created.
        /// </param>
        /// <returns>
        /// The created <see cref="Facility"/> entity.
        /// </returns>
        Task<Facility> Execute(Facility facility);
    }
}