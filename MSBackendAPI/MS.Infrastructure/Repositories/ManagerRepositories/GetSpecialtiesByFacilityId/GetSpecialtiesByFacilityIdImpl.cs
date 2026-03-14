using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.ManagerRepositories.GetSpecialtiesByFacilityId
{
    /// <summary>
    /// Implementation to get active specialties for a specific facility
    /// </summary>
    public class GetSpecialtiesByFacilityIdImpl : IGetSpecialtiesByFacilityId
    {
        private readonly AppDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Database context</param>
        public GetSpecialtiesByFacilityIdImpl(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Execute the query to find active specialties by facility ID
        /// </summary>
        /// <param name="facilityId">The facility ID to query</param>
        /// <returns>List of FacilitySpecialty including Specialty details</returns>
        public async Task<List<FacilitySpecialty>> Execute(Guid facilityId)
        {
            return await _context.Set<FacilitySpecialty>()
                .AsNoTracking()
                .Include(fs => fs.Specialty)
                .Where(fs => fs.FacilityId == facilityId && fs.IsActive)
                .ToListAsync();
        }
    }
}
