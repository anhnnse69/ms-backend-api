using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetSpecialtyForBooking
{
    /// <summary>
    /// Repository implementation for retrieving a specialty by its identifier during appointment booking.
    /// </summary>
    public class GetSpecialtyForBookingImpl : RepositoryQueryBase<Specialty, Guid, AppDbContext>, IGetSpecialtyForBookingRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSpecialtyForBookingImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetSpecialtyForBookingImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the specialty entity matching the specified identifier.
        /// </summary>
        /// <param name="specialtyId">The unique identifier of the specialty.</param>
        /// <returns>Specialty entity if found; otherwise null.</returns>
        public async Task<Specialty?> Execute(Guid specialtyId)
        {
            return await FindByCondition(s => s.Id == specialtyId, false)
                .FirstOrDefaultAsync();
        }
    }
}
