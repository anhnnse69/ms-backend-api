using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.SearchDoctor
{
    /// <summary>
    /// Repository implementation for searching doctors with dynamic filters and pagination.
    /// </summary>
    public class SearchDoctorImpl : RepositoryQueryBase<Doctor, Guid, AppDbContext>, ISearchDoctor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchDoctorImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public SearchDoctorImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a paginated list of doctors matching the specified search criteria.
        /// </summary>
        /// <param name="keyword">Optional keyword to filter by doctor full name or facility name.</param>
        /// <param name="specialtyId">Optional specialty identifier to filter doctors by specialty.</param>
        /// <param name="facilityId">Optional facility identifier to filter doctors by facility.</param>
        /// <param name="location">Optional city or address string to filter by location.</param>
        /// <param name="page">The current page index (1-based).</param>
        /// <param name="size">The number of records per page.</param>
        /// <returns>
        /// A tuple containing the list of matching doctor entities and the total record count.
        /// </returns>
        public async Task<(List<Doctor> doctors, int total)> Execute(
            string? keyword,
            Guid? specialtyId,
            Guid? facilityId,
            string? location,
            int page,
            int size)
        {
            var query = FindAll(false)
                .Include(d => d.Specialty)
                .Include(d => d.Facilities)
                    .ThenInclude(df => df.Facility)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(d =>
                    d.FullName.Contains(keyword) ||
                    d.Facilities.Any(df => df.Facility.NameVi.Contains(keyword)));
            }
            if (specialtyId.HasValue)
            {
                query = query.Where(d => d.SpecialtyId == specialtyId.Value);
            }
            if (facilityId.HasValue)
            {
                query = query.Where(d => d.Facilities.Any(df => df.FacilityId == facilityId.Value));
            }
            if (!string.IsNullOrWhiteSpace(location))
            {
                query = query.Where(d =>
                    d.Facilities.Any(df =>
                        df.Facility.City.Contains(location) ||
                        df.Facility.Address.Contains(location)));
            }
            var total = await query.CountAsync();
            var doctors = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return (doctors, total);
        }
    }
}
