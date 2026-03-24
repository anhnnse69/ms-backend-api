using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.PatientRepositories.GetProfileById;

namespace MS.Application.Services.PatientServices.GetUserProfileService
{
    /// <summary>
    /// Service implementation for retrieving the authenticated user's profile.
    /// </summary>
    public class GetUserProfileService : IGetUserProfileService
    {
        private readonly IGetProfileById _userProfile;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetUserProfileService"/> class.
        /// </summary>
        /// <param name="userProfile">Repository for retrieving user data by identifier.</param>
        public GetUserProfileService(IGetProfileById userProfile)
        {
            _userProfile = userProfile;
        }

        /// <summary>
        /// Processes the request to retrieve the authenticated user's profile.
        /// </summary>
        /// <param name="userId">The unique identifier of the authenticated user.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing <see cref="GetUserProfileResponse"/>
        /// with the user's personal profile information.
        /// </returns>
        public async Task<ApiResponse<GetUserProfileResponse>> Process(Guid userId)
        {
            // 1. Initialize validation flags
            bool isUserValid = true;
            // 2. Retrieve user
            var user = await RetrieveUser(userId);
            // 3. Validate user existence
            ValidateUser(user, ref isUserValid);
            // 4. Map to response
            var response = MapToResponse(user);
            // 5. Return API response
            return CreateResponse(response, isUserValid);
        }

        /// <summary>
        /// Retrieves the user entity by the specified identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns><see cref="User"/> if found; otherwise null.</returns>
        private async Task<User?> RetrieveUser(Guid userId)
        {
            return await _userProfile.Execute(userId);
        }

        /// <summary>
        /// Validates whether the user entity exists.
        /// </summary>
        /// <param name="user">The user entity to validate.</param>
        /// <param name="isUserValid">Validation flag; set to false if the user is null.</param>
        private void ValidateUser(User? user, ref bool isUserValid)
        {
            if (user == null)
            {
                isUserValid = false;
            }
        }

        /// <summary>
        /// Maps a <see cref="User"/> entity to <see cref="GetUserProfileResponse"/>.
        /// </summary>
        /// <param name="user">The user entity to map; may be null.</param>
        /// <returns>Mapped <see cref="GetUserProfileResponse"/>; default instance if user is null.</returns>
        private GetUserProfileResponse MapToResponse(User? user)
        {
            if (user == null)
            {
                return new GetUserProfileResponse();
            }
            return new GetUserProfileResponse
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                FacilityId = user.FacilityId,
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isUserValid">Flag indicating whether the user exists.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<GetUserProfileResponse> CreateResponse(
            GetUserProfileResponse response,
            bool isUserValid)
        {
            if (!isUserValid)
            {
                return ApiResponse<GetUserProfileResponse>.Fail(MessageCode.APP_MESSAGE_4020.ToString());
            }
            return ApiResponse<GetUserProfileResponse>.Success(MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
