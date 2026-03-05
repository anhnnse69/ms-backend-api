using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Infrastructure.Repositories.UserRepositories.GetAllUsers;

namespace MS.Application.Services.UserService.GetAllUsersService
{
    /// <summary>
    /// Get all users service implementation
    /// </summary>
    public class GetAllUsersService : IGetAllUsersService
    {
        private readonly IGetAllUsers _getAllUsers;

        /// <summary>
        /// Get all users service constructor
        /// </summary>
        /// <param name="getAllUsers"></param>
        public GetAllUsersService(IGetAllUsers getAllUsers)
        {
            _getAllUsers = getAllUsers;
        }

        /// <summary>
        /// Process get all users request
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<List<User>>> Process()
        {
            var users = await _getAllUsers.Execute();
            return ApiResponse<List<User>>.Success("APP_MESSAGE_2000", users);
        }
    }
}