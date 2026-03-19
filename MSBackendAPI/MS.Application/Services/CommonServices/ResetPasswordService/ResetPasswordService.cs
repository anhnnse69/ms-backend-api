using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.EmailVerifyService;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;
using MS.Infrastructure.Repositories.PatientRepositories.GetValidPasswordResetToken;
using MS.Infrastructure.Repositories.PatientRepositories.ResetPasswordTransactional;

namespace MS.Application.Services.CommonServices.ResetPasswordService
{
    /// <summary>
    /// Handles the business logic for resetting a user's password via an email token.
    /// Uses a transactional repository to guarantee atomicity when updating password and invalidating token.
    /// </summary>
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IGetUserByEmail _getUserByEmail;
        private readonly IGetValidPasswordResetToken _getValidPasswordResetToken;
        private readonly IResetPasswordTransactional _resetPasswordTransactional;
        private readonly IEmailVerifyService _emailVerifyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordService"/> class.
        /// </summary>
        /// <param name="getUserByEmail">Repository responsible for retrieving a user by email.</param>
        /// <param name="getValidPasswordResetToken">Repository responsible for retrieving a valid, unused, non-expired token.</param>
        /// <param name="resetPasswordTransactional">Repository responsible for atomically updating the password and invalidating the token.</param>
        /// <param name="emailVerifyService">Service responsible for sending notification emails.</param>
        public ResetPasswordService(
            IGetUserByEmail getUserByEmail,
            IGetValidPasswordResetToken getValidPasswordResetToken,
            IResetPasswordTransactional resetPasswordTransactional,
            IEmailVerifyService emailVerifyService)
        {
            _getUserByEmail = getUserByEmail;
            _getValidPasswordResetToken = getValidPasswordResetToken;
            _resetPasswordTransactional = resetPasswordTransactional;
            _emailVerifyService = emailVerifyService;
        }

        /// <summary>
        /// Processes the password reset request by validating the user and token,
        /// then atomically updating the password and invalidating the token.
        /// Sends a security notification email on success.
        /// </summary>
        /// <param name="request">The request containing the email, token, and new password.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> with a success message if the reset succeeded,
        /// or a failure response with the corresponding error code.
        /// </returns>
        public async Task<ApiResponse<ResetPasswordResponse>> Process(ResetPasswordRequest request)
        {
            // 1. Initialize validation flags
            bool isUserValid = true;
            bool isTokenValid = true;
            // 2. Retrieve user and token from repositories
            var user = await RetrieveUser(request.Email);
            ValidateUser(user, ref isUserValid);
            var resetToken = await RetrieveValidToken(user, request.Token, isUserValid);
            // 3. Validate token
            ValidateToken(resetToken, ref isTokenValid);
            // 4. Execute atomic password reset (update password + mark token used)
            await ExecuteResetPassword(user, resetToken, request.NewPassword, isUserValid, isTokenValid);
            // 5. Send security notification email
            await ExecuteSendNotification(user, isUserValid, isTokenValid);
            // 6. Map to response
            var response = MapToResponse();
            // 7. Return API response
            return CreateResponse(response, isUserValid, isTokenValid);
        }

        /// <summary>
        /// Retrieves a user entity by their email address.
        /// </summary>
        /// <param name="email">The email address to search for.</param>
        /// <returns>The <see cref="User"/> if found and not deleted; otherwise null.</returns>
        private async Task<User?> RetrieveUser(string email)
        {
            return await _getUserByEmail.Execute(email);
        }

        /// <summary>
        /// Retrieves a valid, unused, non-expired password reset token for the specified user.
        /// Returns null immediately if the user flag is invalid to skip unnecessary DB calls.
        /// </summary>
        /// <param name="user">The user entity resolved from the email.</param>
        /// <param name="token">The raw token string provided in the request.</param>
        /// <param name="isUserValid">Flag indicating whether the user was successfully resolved.</param>
        /// <returns>The matching <see cref="PasswordResetToken"/> if found; otherwise null.</returns>
        private async Task<PasswordResetToken?> RetrieveValidToken(User? user, string token, bool isUserValid)
        {
            if (!isUserValid || user == null)
            {
                return null;
            }
            return await _getValidPasswordResetToken.Execute(user.Id, token);
        }

        /// <summary>
        /// Validates whether the user exists in the system.
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
        /// Validates whether the password reset token is valid, unused, and not expired.
        /// </summary>
        /// <param name="resetToken">The token entity to validate.</param>
        /// <param name="isTokenValid">Validation flag; set to false if the token is null.</param>
        private void ValidateToken(PasswordResetToken? resetToken, ref bool isTokenValid)
        {
            if (resetToken == null)
            {
                isTokenValid = false;
            }
        }

        /// <summary>
        /// Executes the atomic password update and token invalidation when all validation flags are valid.
        /// Delegates to the transactional repository which wraps both operations in a single DB transaction.
        /// </summary>
        /// <param name="user">The user whose password will be updated.</param>
        /// <param name="resetToken">The token to be marked as used.</param>
        /// <param name="newPassword">The plain-text new password to be hashed and stored.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid.</param>
        /// <param name="isTokenValid">Flag indicating whether the token is valid.</param>
        private async Task ExecuteResetPassword(
            User? user,
            PasswordResetToken? resetToken,
            string newPassword,
            bool isUserValid,
            bool isTokenValid)
        {
            if (!isUserValid || !isTokenValid || user == null || resetToken == null)
            {
                return;
            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _resetPasswordTransactional.Execute(user, passwordHash, resetToken);
        }

        /// <summary>
        /// Executes sending of a security notification email when all validation flags are valid.
        /// </summary>
        /// <param name="user">The user to notify about the password change.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid.</param>
        /// <param name="isTokenValid">Flag indicating whether the token is valid.</param>
        private async Task ExecuteSendNotification(User? user, bool isUserValid, bool isTokenValid)
        {
            if (!isUserValid || !isTokenValid || user == null)
            {
                return;
            }
            await _emailVerifyService.SendPasswordChangedNotificationAsync(user.Email, user.FullName);
        }

        /// <summary>
        /// Maps the result to a <see cref="ResetPasswordResponse"/> with a success message code.
        /// </summary>
        /// <returns>A <see cref="ResetPasswordResponse"/> with a message code confirmation.</returns>
        private ResetPasswordResponse MapToResponse()
        {
            return new ResetPasswordResponse
            {
                Message = MessageCode.APP_MESSAGE_2000.ToString()
            };
        }

        /// <summary>
        /// Constructs the final <see cref="ApiResponse{T}"/> based on validation flags.
        /// </summary>
        /// <param name="response">The mapped response data.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid.</param>
        /// <param name="isTokenValid">Flag indicating whether the token is valid.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<ResetPasswordResponse> CreateResponse(
            ResetPasswordResponse response,
            bool isUserValid,
            bool isTokenValid)
        {
            if (!isUserValid)
                return ApiResponse<ResetPasswordResponse>.Fail(MessageCode.APP_MESSAGE_4020.ToString());
            if (!isTokenValid)
                return ApiResponse<ResetPasswordResponse>.Fail(MessageCode.APP_MESSAGE_4019.ToString());
            return ApiResponse<ResetPasswordResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
