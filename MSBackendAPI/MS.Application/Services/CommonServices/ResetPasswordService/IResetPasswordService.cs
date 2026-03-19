using MS.Application.Common.Response;

namespace MS.Application.Services.CommonServices.ResetPasswordService
{
    /// <summary>
    /// Defines the contract for the reset password service.
    /// </summary>
    public interface IResetPasswordService
    {
        /// <summary>
        /// Processes the request to reset a user's password using a valid scoped reset JWT.
        /// </summary>
        /// <param name="request">The request containing the scoped reset JWT and new password details.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a <see cref="ResetPasswordResponse"/>
        /// with a confirmation message on success, or an error response if validation fails.
        /// </returns>
        Task<ApiResponse<ResetPasswordResponse>> Process(ResetPasswordRequest request);
    }
}
