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
        /// Get user by id service constructor
        /// </summary>
        /// <param name="getUserById"></param>
        public GetUserByIdService(IGetUserById getUserById)
        {
            _getUserById = getUserById;
        }

        /// <summary>
        /// Process get user by id request
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns></returns>
        public async Task<ApiResponse<User>> Process(Guid id)
        {
            var user = await _getUserById.Execute(id);

            if (user == null)
                return ApiResponse<User>.Fail(MessageCode.APP_MESSAGE_4020.ToString());

            return ApiResponse<User>.Success("APP_MESSAGE_2000", user);
        }
    }
}