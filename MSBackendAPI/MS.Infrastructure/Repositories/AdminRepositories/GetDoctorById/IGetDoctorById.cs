using MS.Domain.Entities;

namespace MS.Application.Services.AdminServices.GetDoctorById
{
    /// <summary>
    /// Defines the contract for retrieving doctor information by ID.
    /// </summary>
    public interface IGetDoctorById
    {
        /// <summary>
        /// Retrieves a doctor by their unique identifier.
        /// </summary>
        /// <param name="id">
        /// The doctor ID.
        /// </param>
        /// <returns>
        /// A <see cref="Doctor"/> entity if found; otherwise null.
        /// </returns>
        Task<Doctor> Execute(Guid id);
    }
}