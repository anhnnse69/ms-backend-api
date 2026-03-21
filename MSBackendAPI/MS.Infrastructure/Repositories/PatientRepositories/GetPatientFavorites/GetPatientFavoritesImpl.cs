using Microsoft.EntityFrameworkCore;
using MS.Domain.Entities;
using MS.Infrastructure.Common.Contracts;
using MS.Infrastructure.Persistence;

namespace MS.Infrastructure.Repositories.PatientRepositories.GetPatientFavorites
{
    /// <summary>
    /// Repository implementation for retrieving a patient's favorites list with filtering and pagination.
    /// </summary>
    public class GetPatientFavoritesImpl : RepositoryQueryBase<Favorite, Guid, AppDbContext>, IGetPatientFavorites
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPatientFavoritesImpl"/> class.
        /// </summary>
        /// <param name="context">Application database context.</param>
        public GetPatientFavoritesImpl(AppDbContext context) : base(context) { }

        /// <summary>
        /// Retrieves a paginated list of active favorites for the specified patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="type">Filter type: "Doctor", "Facility", or "All".</param>
        /// <param name="page">The current page index (1-based).</param>
        /// <param name="size">The number of records per page.</param>
        /// <returns>Tuple of favorites list and total count matching the filter.</returns>
        public async Task<(List<Favorite> favorites, int total)> Execute(Guid patientId, string type, int page, int size)
        {
            var query = FindByCondition(f => f.PatientId == patientId, false)
                .Include(f => f.Doctor).ThenInclude(d => d.Specialty)
                .Include(f => f.Facility)
                .AsQueryable();
            var normalizedType = type?.Trim().ToLower();
            if (normalizedType == "doctor")
            {
                query = query.Where(f => f.DoctorId != null);
            }
            else if (normalizedType == "facility")
            {
                query = query.Where(f => f.FacilityId != null);
            }
            query = query.OrderByDescending(f => f.CreateDate);
            var total = await query.CountAsync();
            var favorites = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            return (favorites, total);
        }
    }
}
