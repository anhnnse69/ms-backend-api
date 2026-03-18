using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetDoctorForBooking
{
    /// <summary>
    /// Repository interface for retrieving a doctor by its identifier during appointment booking.
    /// </summary>
    public interface IGetDoctorForBooking
    {
        /// <summary>
        /// Retrieves the doctor entity matching the specified identifier.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>Doctor entity if found; otherwise null.</returns>
        Task<Doctor?> Execute(Guid doctorId);
    }
}
