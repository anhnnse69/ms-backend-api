using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllFacilities;

namespace MS.Application.Services.AdminServices.GetAllFacilitiesService
{
    /// <summary>
    /// Get all facilities service implementation
    /// </summary>
    public class GetAllFacilitiesService : IGetAllFacilitiesService
    {
        private readonly IGetAllFacilities _getAllFacilities;

        /// <summary>
        /// Initializes a new instance of the GetAllFacilitiesService class.
        /// </summary>
        /// <param name="getAllFacilities">
        /// Repository used to retrieve facility data.
        /// </param>
        public GetAllFacilitiesService(IGetAllFacilities getAllFacilities)
        {
            _getAllFacilities = getAllFacilities;
        }

        /// <summary>
        /// Process get all facilities request
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="size">The number of records per page.</param>
        /// <returns></returns>
        public async Task<ApiResponse<IEnumerable<GetAllFacilitiesResponse>>> Process(int page, int size)
        {
            // 1. Initialize validation flag
            bool isRetrievedDataValid = true;
            // 2. Retrieve facility query from repository
            var query = RetrieveQuery();
            // 3. Retrieve paginated facility data based on page and size
            var data = await RetrieveData(query, page, size);
            // 4. Retrieve total number of facilities for pagination metadata
            var total = await RetrieveTotal(query);
            // 5. Validate retrieved data
            ValidateData(query, ref isRetrievedDataValid);
            // 6. Create and return response
            return CreateResponse(data, total, page, size, isRetrievedDataValid);
        }

        /// <summary>
        /// Retrieve facility query
        /// </summary>
        private IQueryable<Facility> RetrieveQuery()
        {
            return _getAllFacilities.Execute();
        }

        /// <summary>
        /// Retrieve paginated facility data
        /// </summary>
        private async Task<List<Facility>> RetrieveData(IQueryable<Facility> query, int page, int size)
        {
            if (query == null)
            {
                return null;
            }
            return await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieve total number of facilities
        /// </summary>
        private async Task<int> RetrieveTotal(IQueryable<Facility> query)
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
        private void ValidateData(IQueryable<Facility> query, ref bool isValid)
        {
            if (query == null)
            {
                isValid = false;
            }
        }

        /// <summary>
        /// Map facility entity to response model
        /// </summary>
        private IEnumerable<GetAllFacilitiesResponse> MapToResponse(IEnumerable<Facility> facilities)
        {
            return facilities.Select(x => new GetAllFacilitiesResponse
            {
                NameVi = x.NameVi,
                NameEn = x.NameEn,
                Address = x.Address,
                Phone = x.Phone,
                Email = x.Email,
                City = x.City,
                Type = x.Type.ToString(),
                IsDeleted = x.IsDeleted,
                LogoUrl = x.LogoUrl
            });
        }

        /// <summary>
        /// Create API response
        /// </summary>
        private ApiResponse<IEnumerable<GetAllFacilitiesResponse>> CreateResponse( IEnumerable<Facility> data, int total, int page, int size, bool isValid)
        {
            if (!isValid)
            {
                return ApiResponse<IEnumerable<GetAllFacilitiesResponse>>
                    .Fail("APP_MESSAGE_4008");
            }
            var result = MapToResponse(data);
            var meta = new MetaResponse(page, size, total);
            return ApiResponse<IEnumerable<GetAllFacilitiesResponse>>
                .Success("APP_MESSAGE_2000", result, meta);
        }
    }
}