using MS.Application.Common.Response;

namespace MS.Application.Services.PatientServices.GetUserProfileService
{
    /// <summary>
    /// Interface for handling user profile retrieval requests.
    /// </summary>
    public interface IGetUserProfileService
    {
        /// <summary>
        /// Processes the request to retrieve the authenticated user's profile.
        /// </summary>
        /// <param name="userId">The unique identifier of the authenticated user.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing <see cref="GetUserProfileResponse"/>
        /// with the user's personal profile information.
        /// </returns>
        Task<ApiResponse<GetUserProfileResponse>> Process(Guid userId);
    }
}
