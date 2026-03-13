using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllSpecialties;

namespace MS.Application.Services.AdminServices.GetAllSpecialtiesService
{
    /// <summary>
    /// Implementation of service used to retrieve all specialties.
    /// </summary>
    public class GetAllSpecialtiesService : IGetAllSpecialtiesService
    {
        private readonly IGetAllSpecialties _getAllSpecialties;

        /// <summary>
        /// Initializes a new instance of GetAllSpecialtiesService.
        /// </summary>
        public GetAllSpecialtiesService(IGetAllSpecialties getAllSpecialties)
        {
            _getAllSpecialties = getAllSpecialties;
        }

        /// <summary>
        /// Process request to retrieve all specialties with pagination.
        /// </summary>
        public async Task<ApiResponse<IEnumerable<GetAllSpecialtiesResponse>>> Process(int page, int size)
        {
            // 1. Initialize validation flag
            bool isRetrievedDataValid = true;
            // 2. Retrieve specialty query from repository
            var query = RetrieveQuery();
            // 3. Retrieve paginated specialty data
            var data = await RetrieveData(query, page, size);
            // 4. Retrieve total number of records
            var total = await RetrieveTotal(query);
            // 5. Validate retrieved query
            ValidateData(query, ref isRetrievedDataValid);
            // 6. Create and return API response
            return CreateResponse(data, total, page, size, isRetrievedDataValid);
        }

        /// <summary>
        /// Retrieve specialty query from repository
        /// </summary>
        private IQueryable<Specialty> RetrieveQuery()
        {
            return _getAllSpecialties.Execute();
        }

        /// <summary>
        /// Retrieve paginated specialty data
        /// </summary>
        private async Task<List<Specialty>> RetrieveData(IQueryable<Specialty> query, int page, int size)
        {
            if (query == null)
            {
                return null;
            }
            return await query
                .OrderBy(x => x.DisplayOrder) // sort specialties by display order
                .Skip((page - 1) * size)     // skip records based on page index
                .Take(size)                  // take limited records for current page
                .ToListAsync();
        }

        /// <summary>
        /// Retrieve total number of specialty records
        /// </summary>
        private async Task<int> RetrieveTotal(IQueryable<Specialty> query)
        {
            if (query == null)
            {
                return 0;
            }

            return await query.CountAsync();
        }

        /// <summary>
        /// Validate retrieved query data
        /// </summary>
        private void ValidateData(IQueryable<Specialty> query, ref bool isValid)
        {
            if (query == null)
            {
                isValid = false;
            }
        }

        /// <summary>
        /// Map specialty entities to response models
        /// </summary>
        private IEnumerable<GetAllSpecialtiesResponse> MapToResponse(IEnumerable<Specialty> specialties)
        {
            return specialties.Select(x => new GetAllSpecialtiesResponse
            {
                NameVi = x.NameVi,
                NameEn = x.NameEn,
                DescriptionVi = x.DescriptionVi,
                DescriptionEn = x.DescriptionEn,
                IconUrl = x.IconUrl,
                IsDeleted = x.IsDeleted
            });
        }

        /// <summary>
        /// Create API response with metadata
        /// </summary>
        private ApiResponse<IEnumerable<GetAllSpecialtiesResponse>> CreateResponse(
            IEnumerable<Specialty> data,
            int total,
            int page,
            int size,
            bool isValid)
        {
            if (!isValid)
            {
                return ApiResponse<IEnumerable<GetAllSpecialtiesResponse>>
                    .Fail("APP_MESSAGE_4009");
            }
            // map entity list to response DTO
            var result = MapToResponse(data);
            // create pagination metadata
            var meta = new MetaResponse(page, size, total);
            // return success response
            return ApiResponse<IEnumerable<GetAllSpecialtiesResponse>>
                .Success("APP_MESSAGE_2000", result, meta);
        }
    }
}