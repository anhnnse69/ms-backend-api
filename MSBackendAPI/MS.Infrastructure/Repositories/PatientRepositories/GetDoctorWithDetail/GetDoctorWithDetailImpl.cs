using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetDoctorWithDetail
{
    /// <summary>
    /// Repository implementation for retrieving a doctor with full detail data.
    /// </summary>
    public class GetDoctorWithDetailImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, IGetDoctorWithDetail
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetDoctorWithDetailImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetDoctorWithDetailImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves the doctor entity with all related detail data by its identifier.
        /// </summary>
        /// <param name="doctorId">The unique identifier of the doctor.</param>
        /// <returns>
        /// Doctor entity including specialty, facilities, and availabilities if found;
        /// otherwise null.
        /// </returns>
        public async Task<Doctor?> Execute(Guid doctorId)
        {
            return await FindByCondition(d => d.Id == doctorId, false)
                .Include(d => d.Specialty)
                .Include(d => d.Facilities)
                    .ThenInclude(df => df.Facility)
                .Include(d => d.Availabilities)
                .FirstOrDefaultAsync();
        }
    }
}
