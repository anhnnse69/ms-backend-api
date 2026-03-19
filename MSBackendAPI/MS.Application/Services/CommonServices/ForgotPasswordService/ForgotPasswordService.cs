using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.EmailVerifyService;
using MS.Infrastructure.Repositories.PatientRepositories.CreatePasswordResetToken;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;

namespace MS.Application.Services.CommonServices.ForgotPasswordService
{
    /// <summary>
    /// Handles the business logic for initiating a password reset request.
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
        /// <param name="createPasswordResetToken">Repository responsible for persisting password reset tokens.</param>
        /// <param name="emailVerifyService">Service responsible for sending emails.</param>
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
        /// Processes the forgot password request by looking up the user, generating a reset token,
        /// persisting it, and sending a reset link via email.
        /// Always returns a success response regardless of whether the email exists to prevent email enumeration.
        /// </summary>
        /// <param name="request">The request containing the user's email address.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> containing a generic success message.
        /// </returns>
        public async Task<ApiResponse<ForgotPasswordResponse>> Process(ForgotPasswordRequest request)
        {
            // 1. Initialize validation flags
            bool isRequestValid = true;
            // 2. Retrieve user by email
            var user = await RetrieveUserByEmail(request.Email);
            // 3. Validate retrieved user (silent — do not expose email enumeration)
            ValidateUserExists(user, ref isRequestValid);
            // 4. Generate and persist password reset token
            var token = await ExecuteCreateToken(user, isRequestValid);
            // 5. Send reset email notification
            await ExecuteSendEmail(user, token, request.Email, isRequestValid);
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
        /// <param name="isRequestValid">Validation flag; set to false if the user is null.</param>
        private void ValidateUserExists(User? user, ref bool isRequestValid)
        {
            if (user == null)
            {
                isRequestValid = false;
            }
        }

        /// <summary>
        /// Executes creation and persistence of a password reset token when validation passes.
        /// </summary>
        /// <param name="user">The user for whom the token is being generated.</param>
        /// <param name="isRequestValid">Flag indicating whether the user is valid.</param>
        /// <returns>The generated token string if created; otherwise null.</returns>
        private async Task<string?> ExecuteCreateToken(User? user, bool isRequestValid)
        {
            if (!isRequestValid || user == null)
            {
                return null;
            }
            var tokenValue = Guid.NewGuid().ToString("N");
            var resetToken = new PasswordResetToken
            {
                UserId = user.Id,
                Token = tokenValue,
                CreatedAt = DateTimeOffset.UtcNow,
                ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30),
                IsUsed = false
            };
            await _createPasswordResetToken.Execute(resetToken);
            return tokenValue;
        }

        /// <summary>
        /// Executes sending of the password reset email when validation passes.
        /// </summary>
        /// <param name="user">The user receiving the reset email.</param>
        /// <param name="token">The generated password reset token.</param>
        /// <param name="email">The destination email address.</param>
        /// <param name="isRequestValid">Flag indicating whether the request is valid.</param>
        private async Task ExecuteSendEmail(User? user, string? token, string email, bool isRequestValid)
        {
            if (!isRequestValid || user == null || token == null)
            {
                return;
            }
            var resetLink = $"https://yourdomain.com/reset-password?email={Uri.EscapeDataString(email)}&token={token}";
            await _emailVerifyService.SendPasswordResetEmailAsync(email, user.FullName, resetLink);
        }

        /// <summary>
        /// Maps the result to a <see cref="ForgotPasswordResponse"/> with a generic message code.
        /// </summary>
        /// <returns>A <see cref="ForgotPasswordResponse"/> with a generic confirmation message code.</returns>
        private ForgotPasswordResponse MapToResponse()
        {
            return new ForgotPasswordResponse
            {
                Message = MessageCode.APP_MESSAGE_2000.ToString()
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
