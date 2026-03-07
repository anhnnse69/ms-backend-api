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
        public async Task<ApiResponse<List<User>>> Process()
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
        private ApiResponse<List<User>> CreateResponse(List<User> users)
        {
            return ApiResponse<List<User>>.Success("APP_MESSAGE_2000", users);
        }
    }
}