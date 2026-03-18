using MS.Domain.Entities;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetFacilityForBooking
{
    /// <summary>
    /// Repository interface for retrieving a facility by its identifier during appointment booking.
    /// </summary>
    public interface IGetFacilityForBookingRepository
    {
        /// <summary>
        /// Retrieves the facility entity matching the specified identifier.
        /// </summary>
        /// <param name="facilityId">The unique identifier of the facility.</param>
        /// <returns>Facility entity if found; otherwise null.</returns>
        Task<Facility?> Execute(Guid facilityId);
    }
}
