using MS.Application.Common.Response;

namespace MS.Application.Services.CommonServices.VerifyOtpService
{
    /// <summary>
    /// Defines the contract for the OTP verification service.
    /// </summary>
    public interface IVerifyOtpService
    {
        /// <summary>
        /// Processes the OTP verification request and issues a short-lived reset token on success.
        /// </summary>
        /// <param name="request">The request containing the user's email and OTP code.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a <see cref="VerifyOtpResponse"/>
        /// with a scoped reset JWT on success, or a failure response if validation fails.
        /// </returns>
        Task<ApiResponse<VerifyOtpResponse>> Process(VerifyOtpRequest request);
    }
}
