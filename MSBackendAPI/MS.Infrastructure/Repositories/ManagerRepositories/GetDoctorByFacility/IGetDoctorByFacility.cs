using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorByFacility
{
    /// <summary>
    /// Repository contract for retrieving doctors by facility
    /// </summary>
    public interface IGetDoctorByFacility
    {
        /// <summary>
        /// Retrieves doctors belonging to a specific facility with pagination
        /// </summary>
        /// <param name="facilityId">Facility identifier used to filter doctors</param>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>
        /// A tuple containing the list of doctors and the total number of records
        /// </returns>
        Task<(List<Doctor> Doctors, int Total)> Execute(Guid facilityId, int page, int size);
    }
}