using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Repositories.AdminRepositories.GetUserById;
using MS.Infrastructure.Repositories.PatientRepositories.UpdateUserProfile;

namespace MS.Application.Services.PatientServices.UpdateUserProfileService
{
    /// <summary>
    /// Service implementation for updating the authenticated user's personal profile.
    /// </summary>
    public class UpdateUserProfileService : IUpdateUserProfileService
    {
        private readonly IGetUserById _userProfile;
        private readonly IUpdateUserProfile _userUpdateRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserProfileService"/> class.
        /// </summary>
        /// <param name="userLookup">Repository for retrieving user data by identifier.</param>
        /// <param name="userUpdateRepository">Repository for persisting user profile updates.</param>
        public UpdateUserProfileService(
            IGetUserById userProfile,
            IUpdateUserProfile userUpdateRepository)
        {
            _userProfile = userProfile;
            _userUpdateRepository = userUpdateRepository;
        }

        /// <summary>
        /// Processes the request to update the authenticated user's personal profile.
        /// </summary>
        /// <param name="request">The request containing the updated profile fields.</param>
        /// <param name="userId">The unique identifier of the authenticated user.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a boolean indicating
        /// whether the update was successful.
        /// </returns>
        public async Task<ApiResponse<bool>> Process(UpdateUserProfileRequest request, Guid userId)
        {
            // 1. Initialize validation flags
            bool isUserValid = true;
            // 2. Retrieve user
            var user = await RetrieveUser(userId);
            // 3. Validate user existence
            ValidateUser(user, ref isUserValid);
            // 4. Execute profile update
            await ExecuteUpdate(user, request, isUserValid);
            // 5. Return API response
            return CreateResponse(isUserValid);
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
        /// Executes the profile update by applying new field values and persisting the changes.
        /// </summary>
        /// <param name="user">The user entity to update; skipped if null.</param>
        /// <param name="request">The request containing the new field values.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid; update is skipped if false.</param>
        private async Task ExecuteUpdate(User? user, UpdateUserProfileRequest request, bool isUserValid)
        {
            if (!isUserValid || user == null)
            {
                return;
            }
            user.DisplayName = request.DisplayName;
            user.FullName = request.FullName;
            user.PhoneNumber = request.PhoneNumber;
            user.AvatarUrl = request.AvatarUrl;
            await _userUpdateRepository.Execute(user);
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="isUserValid">Flag indicating whether the user exists and was updated.</param>
        /// <returns>
        /// Success response with true if the update succeeded; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<bool> CreateResponse(bool isUserValid)
        {
            if (!isUserValid)
            {
                return ApiResponse<bool>.Fail(MessageCode.APP_MESSAGE_4020.ToString());
            }
            return ApiResponse<bool>.Success(MessageCode.APP_MESSAGE_2006.ToString(), true);
        }
    }
}
