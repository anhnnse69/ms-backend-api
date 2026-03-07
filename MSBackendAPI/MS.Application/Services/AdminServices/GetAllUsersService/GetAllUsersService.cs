using Microsoft.EntityFrameworkCore;
using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllUsers;

namespace MS.Application.Services.AdminServices.GetAllUsersService
{
    /// <summary>
    /// Service responsible for retrieving all users with pagination support
    /// </summary>
    public class GetAllUsersService : IGetAllUsersService
    {
        private readonly IGetAllUsers _getAllUsers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="getAllUsers">
        /// Repository used to retrieve user query from database
        /// </param>
        public GetAllUsersService(IGetAllUsers getAllUsers)
        {
            _getAllUsers = getAllUsers;
        }

        /// <summary>
        /// Process get all users request
        /// </summary>
        /// <param name="page">Current page index</param>
        /// <param name="size">Number of records per page</param>
        /// <returns>Returns paginated list of users</returns>
        public async Task<ApiResponse<IEnumerable<GetAllUsersResponse>>> Process(int page, int size)
        {
            // 1. Initialize validation flag
            bool isRetrievedDataValid = true;

            // 2. Retrieve user query
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
        /// Retrieve user query from repository
        /// </summary>
        private IQueryable<User> RetrieveQuery()
        {
            return _getAllUsers.Execute();
        }

        /// <summary>
        /// Retrieve paginated user data
        /// </summary>
        private async Task<List<User>> RetrieveData(IQueryable<User> query, int page, int size)
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
        /// Retrieve total number of users
        /// </summary>
        private async Task<int> RetrieveTotal(IQueryable<User> query)
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
        private void ValidateData(IQueryable<User> query, ref bool isValid)
        {
            if (query == null)
            {
                isValid = false;
            }
        }

        /// <summary>
        /// Map user entities to response models
        /// </summary>
        private IEnumerable<GetAllUsersResponse> MapToResponse(IEnumerable<User> users)
        {
            return users.Select(x => new GetAllUsersResponse
            {
                Id = x.Id,
                Username = x.Username,
                DisplayName = x.DisplayName,
                FullName = x.FullName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                AvatarUrl = x.AvatarUrl,
                Role = x.Role.ToString(),
                IsActive = x.IsActive
            });
        }

        /// <summary>
        /// Create API response with pagination metadata
        /// </summary>
        private ApiResponse<IEnumerable<GetAllUsersResponse>> CreateResponse(IEnumerable<User> data, int total, int page, int size, bool isValid)
        {
            if (!isValid)
            {
                return ApiResponse<IEnumerable<GetAllUsersResponse>>
                    .Fail("APP_MESSAGE_4020");
            }
            // Map entity to response DTO
            var result = MapToResponse(data);
            // Create pagination metadata
            var meta = new MetaResponse(page, size, total);
            // Return success response
            return ApiResponse<IEnumerable<GetAllUsersResponse>>
                .Success("APP_MESSAGE_2000", result, meta);
        }
    }
}