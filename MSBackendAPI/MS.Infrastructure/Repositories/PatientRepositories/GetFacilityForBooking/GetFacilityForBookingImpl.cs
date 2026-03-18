using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetFacilityForBooking
{
    /// <summary>
    /// Repository implementation for retrieving a facility by its identifier during appointment booking.
    /// </summary>
    public class GetFacilityForBookingImpl : RepositoryQueryBase<Facility, Guid, AppDbContext>, IGetFacilityForBookingRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFacilityForBookingImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetFacilityForBookingImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the facility entity matching the specified identifier.
        /// </summary>
        /// <param name="facilityId">The unique identifier of the facility.</param>
        /// <returns>Facility entity if found; otherwise null.</returns>
        public async Task<Facility?> Execute(Guid facilityId)
        {
            return await FindByCondition(f => f.Id == facilityId, false)
                .FirstOrDefaultAsync();
        }
    }
}
