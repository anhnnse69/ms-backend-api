using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetDoctorDetailByFacility
{
    /// <summary>
    /// Repository contract for retrieving doctor detail information
    /// </summary>
    public interface IGetDoctorDetailByFacility
    {
        /// <summary>
        /// Retrieve doctor detail by identifier
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>Doctor entity if found, otherwise null</returns>
        Task<Doctor?> Execute(Guid doctorId);
    }
}