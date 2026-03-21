using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Common.Services.JwtResetToken;
using MS.Infrastructure.Repositories.PatientRepositories.GetValidPasswordResetTokenByOtp;
using MS.Infrastructure.Repositories.PatientRepositories.MarkOtpUsed;

namespace MS.Application.Services.CommonServices.VerifyOtpService
{
    /// <summary>
    /// Handles the business logic for verifying a password reset OTP and issuing a scoped reset JWT.
    /// Resolves the user directly from the OTP token record to avoid requiring the client
    /// to re-submit the email address.
    /// Marks the OTP as used immediately upon successful verification to enforce one-time use.
    /// </summary>
    public class VerifyOtpService : IVerifyOtpService
    {
        private readonly IGetValidPasswordResetTokenByOtp _getValidPasswordResetTokenByOtp;
        private readonly IMarkOtpUsed _markOtpUsed;
        private readonly IJwtResetTokenService _jwtResetTokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerifyOtpService"/> class.
        /// </summary>
        /// <param name="getValidPasswordResetTokenByOtp">Repository responsible for retrieving a valid OTP token with its associated user.</param>
        /// <param name="markOtpUsed">Repository responsible for marking the OTP token as used.</param>
        /// <param name="jwtResetTokenService">Service responsible for generating scoped reset JWTs.</param>
        public VerifyOtpService(
            IGetValidPasswordResetTokenByOtp getValidPasswordResetTokenByOtp,
            IMarkOtpUsed markOtpUsed,
            IJwtResetTokenService jwtResetTokenService)
        {
            _getValidPasswordResetTokenByOtp = getValidPasswordResetTokenByOtp;
            _markOtpUsed = markOtpUsed;
            _jwtResetTokenService = jwtResetTokenService;
        }

        /// <summary>
        /// Processes the OTP verification request by retrieving the token with its associated user,
        /// marking the OTP as used, and issuing a short-lived scoped reset JWT.
        /// </summary>
        /// <param name="request">The request containing the OTP code.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a <see cref="VerifyOtpResponse"/>
        /// with a scoped reset JWT on success, or a failure response if validation fails.
        /// </returns>
        public async Task<ApiResponse<VerifyOtpResponse>> Process(VerifyOtpRequest request)
        {
            // 1. Initialize validation flags
            bool isOtpValid = true;
            bool isUserValid = true;
            // 2. Retrieve OTP token with associated user
            var otpToken = await RetrieveValidOtpToken(request.OtpCode);
            // 3. Validate OTP token exists and is valid
            ValidateOtpToken(otpToken, ref isOtpValid);
            // 4. Validate associated user is present and not deleted
            ValidateTokenUser(otpToken, ref isUserValid);
            // 5. Mark OTP as used (one-time use enforcement)
            await ExecuteMarkOtpUsed(otpToken, isOtpValid, isUserValid);
            // 6. Generate scoped reset JWT
            var resetToken = GenerateResetToken(otpToken, isOtpValid, isUserValid);
            // 7. Map to response
            var response = MapToResponse(resetToken, isOtpValid, isUserValid);
            // 8. Return API response
            return CreateResponse(response, isOtpValid, isUserValid);
        }

        /// <summary>
        /// Retrieves a valid, unused, non-expired OTP token with its associated user by OTP code.
        /// </summary>
        /// <param name="otpCode">The raw 6-digit OTP string to match.</param>
        /// <returns>
        /// The matching <see cref="PasswordResetToken"/> with user populated if found; otherwise null.
        /// </returns>
        private async Task<PasswordResetToken?> RetrieveValidOtpToken(string otpCode)
        {
            return await _getValidPasswordResetTokenByOtp.Execute(otpCode);
        }

        /// <summary>
        /// Validates whether the OTP token exists, is unused, and has not expired.
        /// </summary>
        /// <param name="otpToken">The OTP token entity to validate.</param>
        /// <param name="isOtpValid">Validation flag; set to false if the token is null.</param>
        private void ValidateOtpToken(PasswordResetToken? otpToken, ref bool isOtpValid)
        {
            if (otpToken == null)
            {
                isOtpValid = false;
            }
        }

        /// <summary>
        /// Validates whether the user associated with the OTP token exists and is not deleted.
        /// </summary>
        /// <param name="otpToken">The OTP token entity containing the associated user.</param>
        /// <param name="isUserValid">Validation flag; set to false if the associated user is null.</param>
        private void ValidateTokenUser(PasswordResetToken? otpToken, ref bool isUserValid)
        {
            if (otpToken?.User == null)
            {
                isUserValid = false;
            }
        }

        /// <summary>
        /// Executes marking the OTP token as used to enforce one-time use when all validation flags are valid.
        /// </summary>
        /// <param name="otpToken">The OTP token to mark as used.</param>
        /// <param name="isOtpValid">Flag indicating whether the OTP is valid.</param>
        /// <param name="isUserValid">Flag indicating whether the associated user is valid.</param>
        private async Task ExecuteMarkOtpUsed(
            PasswordResetToken? otpToken,
            bool isOtpValid,
            bool isUserValid)
        {
            if (!isOtpValid || !isUserValid || otpToken == null)
            {
                return;
            }
            await _markOtpUsed.Execute(otpToken);
        }

        /// <summary>
        /// Generates a short-lived scoped reset JWT when all validation flags are valid.
        /// </summary>
        /// <param name="otpToken">The OTP token containing the associated user data.</param>
        /// <param name="isOtpValid">Flag indicating whether the OTP is valid.</param>
        /// <param name="isUserValid">Flag indicating whether the associated user is valid.</param>
        /// <returns>The signed reset JWT string if valid; otherwise null.</returns>
        private string? GenerateResetToken(
            PasswordResetToken? otpToken,
            bool isOtpValid,
            bool isUserValid)
        {
            if (!isOtpValid || !isUserValid || otpToken?.User == null)
            {
                return null;
            }
            return _jwtResetTokenService.GenerateResetToken(
                otpToken.User.Id, otpToken.User.Email);
        }

        /// <summary>
        /// Maps the generated reset token to a <see cref="VerifyOtpResponse"/>.
        /// </summary>
        /// <param name="resetToken">The scoped reset JWT string to include in the response.</param>
        /// <param name="isOtpValid">Flag indicating whether the OTP is valid.</param>
        /// <param name="isUserValid">Flag indicating whether the associated user is valid.</param>
        /// <returns>A populated <see cref="VerifyOtpResponse"/>; default instance if any flag is invalid.</returns>
        private VerifyOtpResponse MapToResponse(
            string? resetToken,
            bool isOtpValid,
            bool isUserValid)
        {
            if (!isOtpValid || !isUserValid || resetToken == null)
            {
                return new VerifyOtpResponse();
            }
            return new VerifyOtpResponse
            {
                ResetToken = resetToken
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isOtpValid">Flag indicating whether the OTP is valid.</param>
        /// <param name="isUserValid">Flag indicating whether the associated user is valid.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<VerifyOtpResponse> CreateResponse(
            VerifyOtpResponse response,
            bool isOtpValid,
            bool isUserValid)
        {
            if (!isOtpValid)
                return ApiResponse<VerifyOtpResponse>.Fail(
                    MessageCode.APP_MESSAGE_4019.ToString());
            if (!isUserValid)
                return ApiResponse<VerifyOtpResponse>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString());
            return ApiResponse<VerifyOtpResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
