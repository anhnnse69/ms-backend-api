using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.AdminRepositories.GetAllUsers;

namespace MS.Application.Services.AdminServices.GetAllUsersService
{
    /// <summary>
    /// Get all users service implementation
    /// </summary>
    public class GetAllUsersService : IGetAllUsersService
    {
        private readonly IGetAllUsers _getAllUsers;

        /// <summary>
        /// Constructor
        /// </summary>
        public GetAllUsersService(IGetAllUsers getAllUsers)
        {
            _getAllUsers = getAllUsers;
        }

        /// <summary>
        /// Process get all users request
        /// </summary>
        public async Task<ApiResponse<List<GetAllUsersResponse>>> Process()
        {
            var users = await RetrieveUsers();
            return CreateResponse(users);
        }

        /// <summary>
        /// Retrieve users from repository
        /// </summary>
        private async Task<List<User>> RetrieveUsers()
        {
            return await _getAllUsers.Execute();
        }

        /// <summary>
        /// Create API response
        /// </summary>
        private ApiResponse<List<GetAllUsersResponse>> CreateResponse(List<User> users)
        {
            if (users == null || !users.Any())
            {
                return ApiResponse<List<GetAllUsersResponse>>
                    .Fail("APP_MESSAGE_4020");
            }

            var result = MapToResponse(users);
            var meta = CreateMeta(users);
            return ApiResponse<List<GetAllUsersResponse>>
                .Success("APP_MESSAGE_2000", result,meta);
        }

        /// <summary>
        /// Map user entities to response models
        /// </summary>
        private List<GetAllUsersResponse> MapToResponse(List<User> users)
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
                IsActive = x.IsActive,
            }).ToList();
        }
        /// <summary>
        /// Create meta response
        /// </summary>
        private MetaResponse CreateMeta(List<User> users)
        {
            int page = 1;
            int size = users.Count;
            int total = users.Count;

            return new MetaResponse(page, size, total);
        }
    }
}