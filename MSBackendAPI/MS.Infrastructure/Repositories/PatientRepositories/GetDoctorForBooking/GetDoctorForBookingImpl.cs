using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetDoctorForBooking
{
    /// <summary>
    /// Repository implementation for retrieving a doctor by its identifier.
    /// </summary>
    public class GetDoctorForBookingImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, IGetDoctorForBooking
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDoctorForBookingImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetDoctorForBookingImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the doctor entity matching the specified identifier.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>Doctor entity if found; otherwise null.</returns>
        public async Task<Doctor?> Execute(Guid doctorId)
        {
            return await FindByCondition(d => d.Id == doctorId, false)
                .FirstOrDefaultAsync();
        }
    }
}
