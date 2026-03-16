using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllDoctors;

namespace MS.Application.Services.AdminServices.GetAllDoctorsService
{
    /// <summary>
    /// Service responsible for retrieving all doctors with pagination support
    /// </summary>
    public class GetAllDoctorsService : IGetAllDoctorsService
    {
        private readonly IGetAllDoctors _getAllDoctors;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="getAllDoctors">
        /// Repository used to retrieve doctor query from database
        /// </param>
        public GetAllDoctorsService(IGetAllDoctors getAllDoctors)
        {
            _getAllDoctors = getAllDoctors;
        }

        /// <summary>
        /// Process get all doctors request
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Returns paginated list of doctors</returns>
        public async Task<ApiResponse<IEnumerable<GetAllDoctorsResponse>>> Process(int page, int size)
        {
            // 1. Initialize validation flag
            bool isRetrievedDataValid = true;
            // 2. Retrieve doctor query
            var query = RetrieveQuery();
            // 3. Retrieve paginated data
            var data = await RetrieveData(query, page, size);
            // 4. Retrieve total records
            var total = await RetrieveTotal(query);
            // 5. Validate retrieved query
            ValidateData(query, ref isRetrievedDataValid);
            // 6. Create API response
            return CreateResponse(data, total, page, size, isRetrievedDataValid);
        }

        /// <summary>
        /// Retrieve doctor query from repository
        /// </summary>
        private IQueryable<Doctor> RetrieveQuery()
        {
            return _getAllDoctors.Execute()
                .Include(x => x.Specialty);
        }

        /// <summary>
        /// Retrieve paginated doctor data
        /// </summary>
        private async Task<List<Doctor>> RetrieveData(IQueryable<Doctor> query, int page, int size)
        {
            if (query == null)
            {
                return null;
            }
            return await query
                // Skip records of previous pages
                .Skip((page - 1) * size)
                // Take records for current page
                .Take(size)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieve total number of doctors
        /// </summary>
        private async Task<int> RetrieveTotal(IQueryable<Doctor> query)
        {
            if (query == null)
            {
                return 0;
            }
            return await query.CountAsync();
        }

        /// <summary>
        /// Validate retrieved data
        /// </summary>
        private void ValidateData(IQueryable<Doctor> query, ref bool isValid)
        {
            if (query == null)
            {
                isValid = false;
            }
        }

        /// <summary>
        /// Map doctor entities to response models
        /// </summary>
        private IEnumerable<GetAllDoctorsResponse> MapToResponse(IEnumerable<Doctor> doctors)
        {
            return doctors.Select(x => new GetAllDoctorsResponse
            {
                Id = x.Id,
                DisplayName = x.DisplayName,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                AvatarUrl = x.AvatarUrl,
                YearsOfExperience = x.YearsOfExperience,
                AverageRating = x.AverageRating,
                RatingCount = x.RatingCount,
                Specialty = x.Specialty?.NameEn,
                IsDeleted = x.IsDeleted
            });
        }

        /// <summary>
        /// Create API response with pagination metadata
        /// </summary>
        private ApiResponse<IEnumerable<GetAllDoctorsResponse>> CreateResponse(
            IEnumerable<Doctor> data,
            int total,
            int page,
            int size,
            bool isValid)
        {
            if (!isValid)
            {
                return ApiResponse<IEnumerable<GetAllDoctorsResponse>>
                    .Fail("APP_MESSAGE_4020");
            }
            // Map entity to response DTO
            var result = MapToResponse(data);
            // Create pagination metadata
            var meta = new MetaResponse(page, size, total);
            // Return success response
            return ApiResponse<IEnumerable<GetAllDoctorsResponse>>
                .Success("APP_MESSAGE_2000", result, meta);
        }
    }
}