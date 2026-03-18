namespace MS.Infrastructure.Repositories.ManagerRepositories.GetFacilityIdByManagerId
{
    /// <summary>
    /// Interface for retrieving the facility ID associated with a manager.
    /// </summary>
    public interface IGetFacilityIdByManagerId
    {
        /// <summary>
        /// Executes the retrieval of the facility ID for the given manager.
        /// </summary>
        /// <param name="managerId">The unique identifier of the manager.</param>
        /// <returns>The facility ID if found and the user is a manager; otherwise, null.</returns>
        Task<Guid?> Execute(Guid managerId);
    }
}
