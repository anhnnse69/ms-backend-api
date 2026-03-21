using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.Common.Services.EmailVerifyService;
using MS.Infrastructure.Repositories.PatientRepositories.CreatePasswordResetToken;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;
using System.Security.Cryptography;

namespace MS.Application.Services.CommonServices.ForgotPasswordService
{
    /// <summary>
    /// Handles the business logic for initiating a password reset request via OTP.
    /// Always returns a success response to prevent email enumeration attacks.
    /// </summary>
    public class ForgotPasswordService : IForgotPasswordService
    {
        private readonly IGetUserByEmail _getUserByEmail;
        private readonly ICreatePasswordResetToken _createPasswordResetToken;
        private readonly IEmailVerifyService _emailVerifyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForgotPasswordService"/> class.
        /// </summary>
        /// <param name="getUserByEmail">Repository responsible for retrieving a user by email.</param>
        /// <param name="createPasswordResetToken">Repository responsible for persisting OTP tokens.</param>
        /// <param name="emailVerifyService">Service responsible for sending OTP emails.</param>
        public ForgotPasswordService(
            IGetUserByEmail getUserByEmail,
            ICreatePasswordResetToken createPasswordResetToken,
            IEmailVerifyService emailVerifyService)
        {
            _getUserByEmail = getUserByEmail;
            _createPasswordResetToken = createPasswordResetToken;
            _emailVerifyService = emailVerifyService;
        }

        /// <summary>
        /// Processes the forgot password request by looking up the user, generating a 6-digit OTP,
        /// persisting it, and sending it via email.
        /// Always returns a success response regardless of whether the email exists.
        /// </summary>
        /// <param name="request">The request containing the user's email address.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a generic success message.
        /// </returns>
        public async Task<ApiResponse<ForgotPasswordResponse>> Process(ForgotPasswordRequest request)
        {
            // 1. Initialize validation flags
            bool isUserValid = true;
            // 2. Retrieve user by email
            var user = await RetrieveUserByEmail(request.Email);
            // 3. Validate retrieved user (silent — do not expose email enumeration)
            ValidateUserExists(user, ref isUserValid);
            // 4. Generate and persist OTP token
            var otpCode = await ExecuteCreateOtp(user, isUserValid);
            // 5. Send OTP email notification
            await ExecuteSendOtpEmail(user, otpCode, request.Email, isUserValid);
            // 6. Map to response
            var response = MapToResponse();
            // 7. Return API response
            return CreateResponse(response);
        }

        /// <summary>
        /// Retrieves a user entity by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>The <see cref="User"/> if found and not deleted; otherwise null.</returns>
        private async Task<User?> RetrieveUserByEmail(string email)
        {
            return await _getUserByEmail.Execute(email);
        }

        /// <summary>
        /// Validates whether the user exists in the system.
        /// </summary>
        /// <param name="user">The user entity to validate.</param>
        /// <param name="isUserValid">Validation flag; set to false if the user is null.</param>
        private void ValidateUserExists(User? user, ref bool isUserValid)
        {
            if (user == null)
            {
                isUserValid = false;
            }
        }

        /// <summary>
        /// Executes generation and persistence of a 6-digit OTP token when validation passes.
        /// The OTP is valid for 5 minutes and stored as a <see cref="PasswordResetToken"/>.
        /// </summary>
        /// <param name="user">The user for whom the OTP is generated.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid.</param>
        /// <returns>The generated 6-digit OTP string if created; otherwise null.</returns>
        private async Task<string?> ExecuteCreateOtp(User? user, bool isUserValid)
        {
            if (!isUserValid || user == null)
            {
                return null;
            }
            var otpCode = RandomNumberGenerator.GetInt32(0, 1_000_000).ToString("D6");
            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = otpCode,
                CreatedAt = DateTimeOffset.UtcNow,
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(5),
                IsUsed = false
            };
            await _createPasswordResetToken.Execute(resetToken);
            return otpCode;
        }

        /// <summary>
        /// Executes sending of the OTP email when validation passes.
        /// </summary>
        /// <param name="user">The user receiving the OTP email.</param>
        /// <param name="otpCode">The generated OTP code to send.</param>
        /// <param name="email">The destination email address.</param>
        /// <param name="isUserValid">Flag indicating whether the request is valid.</param>
        private async Task ExecuteSendOtpEmail(
            User? user,
            string? otpCode,
            string email,
            bool isUserValid)
        {
            if (!isUserValid || user == null || otpCode == null)
            {
                return;
            }
            await _emailVerifyService.SendOtpEmailAsync(email, user.FullName, otpCode);
        }

        /// <summary>
        /// Maps the result to a <see cref="ForgotPasswordResponse"/> with a generic message code.
        /// </summary>
        /// <returns>A <see cref="ForgotPasswordResponse"/> with a generic confirmation message code.</returns>
        private ForgotPasswordResponse MapToResponse()
        {
            return new ForgotPasswordResponse
            {
                Message = string.Empty
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> for the forgot password operation.
        /// Always returns success to prevent email enumeration attacks.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <returns>
        /// A success response containing the generic confirmation message.
        /// </returns>
        private ApiResponse<ForgotPasswordResponse> CreateResponse(ForgotPasswordResponse response)
        {
            return ApiResponse<ForgotPasswordResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}