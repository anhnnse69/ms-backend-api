using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.UserRepositories.GetUserById;

namespace MS.Application.Services.UserService.GetUserByIdService
{
    /// <summary>
    /// Get user by id service implementation
    /// </summary>
    public class GetUserByIdService : IGetUserByIdService
    {
        private readonly IGetUserById _getUserById;

        /// <summary>
        /// Constructor
        /// </summary>
        public GetUserByIdService(IGetUserById getUserById)
        {
            _getUserById = getUserById;
        }

        /// <summary>
        /// Process get user by id request
        /// </summary>
        public async Task<ApiResponse<User>> Process(Guid id)
        {
            var user = await RetrieveUser(id);
            return CreateResponse(user);
        }

        /// <summary>
        /// Retrieve user from repository
        /// </summary>
        private async Task<User> RetrieveUser(Guid id)
        {
            return await _getUserById.Execute(id);
        }

        /// <summary>
        /// Create API response
        /// </summary>
        private ApiResponse<User> CreateResponse(User user)
        {
            if (user == null)
            {
                return ApiResponse<User>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString()
                );
            }
            return ApiResponse<User>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                user
            );
        }
    }
}