using MS.Application.Common.Response;

namespace MS.Application.Services.CommonServices.ForgotPasswordService
{
    /// <summary>
    /// Defines the contract for the forgot password service.
    /// </summary>
    public interface IForgotPasswordService
    {
        /// <summary>
        /// Processes the request to initiate a password reset for the given email.
        /// </summary>
        /// <param name="request">The request containing the user's email address.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a <see cref="ForgotPasswordResponse"/>
        /// with a generic confirmation message regardless of whether the email exists.
        /// </returns>
        Task<ApiResponse<ForgotPasswordResponse>> Process(ForgotPasswordRequest request);
    }
}
