using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetDoctorWithDetail
{
    /// <summary>
    /// Repository interface for retrieving a doctor with full detail including
    /// specialty, facilities, and availabilities.
    /// </summary>
    public interface IGetDoctorWithDetail
    {
        /// <summary>
        /// Retrieves the doctor entity with all related detail data by its identifier.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>
        /// Doctor entity including specialty, facilities, and availabilities if found;
        /// otherwise null.
        /// </returns>
        Task<Doctor?> Execute(Guid doctorId);
    }
}
