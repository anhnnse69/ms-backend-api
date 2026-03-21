using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetFavoriteByTarget
{
    /// <summary>
    /// Repository implementation for retrieving an existing favorite record including soft-deleted ones.
    /// </summary>
    public class GetFavoriteByTargetImpl : RepositoryQueryBase<Favorite, Guid, AppDbContext>, IGetFavoriteByTarget
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetFavoriteByTargetImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetFavoriteByTargetImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a favorite record by patient and target, bypassing the global soft-delete filter.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="doctorId">Optional doctor identifier.</param>
        /// <param name="facilityId">Optional facility identifier.</param>
        /// <returns>Matching favorite entity including soft-deleted; otherwise null.</returns>
        public async Task<Favorite?> Execute(Guid patientId, Guid? doctorId, Guid? facilityId)
        {
            return await FindByCondition(
                    f => f.PatientId == patientId
                      && f.DoctorId == doctorId
                      && f.FacilityId == facilityId,
                    false)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync();
        }
    }
}
