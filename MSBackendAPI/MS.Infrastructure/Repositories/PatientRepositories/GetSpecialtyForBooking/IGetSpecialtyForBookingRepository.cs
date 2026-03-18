using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetSpecialtyForBooking
{
    /// <summary>
    /// Repository interface for retrieving a specialty by its identifier during appointment booking.
    /// </summary>
    public interface IGetSpecialtyForBookingRepository
    {
        /// <summary>
        /// Retrieves the specialty entity matching the specified identifier.
        /// </summary>
        /// <param name="specialtyId">The unique identifier of the specialty.</param>
        /// <returns>Specialty entity if found; otherwise null.</returns>
        Task<Specialty?> Execute(Guid specialtyId);
    }
}
