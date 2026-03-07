using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetUserById;

namespace MS.Application.Services.AdminServices.GetUserByIdService
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
        public async Task<ApiResponse<GetUserByIdResponse>> Process(Guid id)
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
        private ApiResponse<GetUserByIdResponse> CreateResponse(User user)
        {
            if (user == null)
            {
                return ApiResponse<GetUserByIdResponse>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString()
                );
            }

            var result = MapToResponse(user);

            return ApiResponse<GetUserByIdResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(),
                result
            );
        }
        /// <summary>
        /// Map user entity to response model
        /// </summary>
        private GetUserByIdResponse MapToResponse(User user)
        {
            return new GetUserByIdResponse
            {
                Id = user.Id,
                Username = user.Username,
                DisplayName = user.DisplayName,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),             
                IsActive = user.IsActive,
            };
        }
    }
}