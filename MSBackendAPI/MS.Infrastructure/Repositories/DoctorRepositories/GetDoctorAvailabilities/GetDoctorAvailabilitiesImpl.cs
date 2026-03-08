using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.DoctorRepositories.GetDoctorAvailabilities
{
    /// <summary>
    /// Repository implementation used to retrieve doctor availability schedule
    /// </summary>
    public class GetDoctorAvailabilitiesImpl
        : RepositoryQueryBase<DoctorAvailability, Guid, AppDbContext>,
          IGetDoctorAvailabilities
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public GetDoctorAvailabilitiesImpl(AppDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Execute query to retrieve availability list of a doctor
        /// </summary>
        /// <param name="doctorId">Doctor identifier</param>
        /// <returns>IQueryable availability query</returns>
        public IQueryable<DoctorAvailability> Execute(Guid doctorId)
        {
            return FindByCondition(x => x.DoctorId == doctorId, false)
                .Include(x => x.Facility);
        }
    }
}