using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.UpdateUserProfileService
{
    /// <summary>
    /// Interface for handling user profile update requests.
    /// </summary>
    public interface IUpdateUserProfileService
    {
        /// <summary>
        /// Processes the request to update the authenticated user's personal profile.
        /// </summary>
        /// <param name="request">The request containing the updated profile fields.</param>
        /// <param name="userId">The unique identifier of the authenticated user.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a boolean indicating
        /// whether the update was successful.
        /// </returns>
        Task<ApiResponse<bool>> Process(UpdateUserProfileRequest request, Guid userId);
    }
}
