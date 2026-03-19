using MS.Application.Common.Response;
using MS.Domain.Entities;
using MS.Domain.Enums.GeneralCodes;
using MS.Domain.Shared.Utility;
using MS.Infrastructure.Common.Services.EmailVerifyService;
using MS.Infrastructure.Common.Services.JwtResetToken;
using MS.Infrastructure.Repositories.PatientRepositories.GetUserByEmail;
using MS.Infrastructure.Repositories.PatientRepositories.GetValidPasswordResetToken;
using MS.Infrastructure.Repositories.PatientRepositories.ResetPasswordTransactional;
using MS.Infrastructure.Repositories.PatientRepositories.UpdatePasswordHash;

namespace MS.Application.Services.CommonServices.ResetPasswordService
{
    /// <summary>
    /// Handles the business logic for resetting a user's password via a scoped reset JWT.
    /// Validates the JWT, updates the password hash, and sends a security notification email.
    /// </summary>
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly IJwtResetTokenService _jwtResetTokenService;
        private readonly IGetUserByEmail _getUserByEmail;
        private readonly IUpdatePasswordHash _updatePasswordHash;
        private readonly IEmailVerifyService _emailVerifyService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResetPasswordService"/> class.
        /// </summary>
        /// <param name="jwtResetTokenService">Service responsible for validating scoped reset JWTs.</param>
        /// <param name="getUserByEmail">Repository responsible for retrieving a user by email.</param>
        /// <param name="updatePasswordHash">Repository responsible for updating the user's password hash.</param>
        /// <param name="emailVerifyService">Service responsible for sending notification emails.</param>
        public ResetPasswordService(
            IJwtResetTokenService jwtResetTokenService,
            IGetUserByEmail getUserByEmail,
            IUpdatePasswordHash updatePasswordHash,
            IEmailVerifyService emailVerifyService)
        {
            _jwtResetTokenService = jwtResetTokenService;
            _getUserByEmail = getUserByEmail;
            _updatePasswordHash = updatePasswordHash;
            _emailVerifyService = emailVerifyService;
        }

        /// <summary>
        /// Processes the password reset request by validating the scoped JWT, updating the
        /// password hash atomically, and sending a security notification email on success.
        /// </summary>
        /// <param name="request">The request containing the scoped reset JWT and new password details.</param>
        /// <returns>
        /// An <see cref="ApiResponse{T}"/> with a success message if the reset succeeded,
        /// or a failure response with the corresponding error code.
        /// </returns>
        public async Task<ApiResponse<ResetPasswordResponse>> Process(ResetPasswordRequest request)
        {
            // 1. Initialize validation flags
            bool isTokenValid = true;
            bool isUserValid = true;
            // 2. Validate and decode the scoped reset JWT
            var claims = RetrieveResetTokenClaims(request.ResetToken);
            // 3. Validate decoded claims
            ValidateResetTokenClaims(claims, ref isTokenValid);
            // 4. Retrieve user from decoded email claim
            var user = await RetrieveUserByEmail(claims, isTokenValid);
            // 5. Validate retrieved user matches token subject
            ValidateUserMatchesToken(user, claims, ref isUserValid);
            // 6. Execute atomic password update
            await ExecuteUpdatePassword(user, request.NewPassword, isTokenValid, isUserValid);
            // 7. Send security notification email
            await ExecuteSendNotification(user, isTokenValid, isUserValid);
            // 8. Map to response
            var response = MapToResponse();
            // 9. Return API response
            return CreateResponse(response, isTokenValid, isUserValid);
        }

        /// <summary>
        /// Retrieves and decodes the userId and email claims from the scoped reset JWT.
        /// </summary>
        /// <param name="resetToken">The signed reset JWT string to decode.</param>
        /// <returns>A tuple of userId and email if the token is valid; otherwise null.</returns>
        private (Guid userId, string email)? RetrieveResetTokenClaims(string resetToken)
        {
            return _jwtResetTokenService.ValidateResetToken(resetToken);
        }

        /// <summary>
        /// Retrieves a user entity by the email extracted from JWT claims.
        /// Returns null immediately if the token flag is invalid to skip unnecessary DB calls.
        /// </summary>
        /// <param name="claims">The decoded JWT claims containing the email address.</param>
        /// <param name="isTokenValid">Flag indicating whether the token was successfully decoded.</param>
        /// <returns>The <see cref="User"/> if found and not deleted; otherwise null.</returns>
        private async Task<User?> RetrieveUserByEmail(
            (Guid userId, string email)? claims,
            bool isTokenValid)
        {
            if (!isTokenValid || claims == null)
            {
                return null;
            }
            return await _getUserByEmail.Execute(claims.Value.email);
        }

        /// <summary>
        /// Validates whether the reset JWT claims are present and successfully decoded.
        /// </summary>
        /// <param name="claims">The decoded JWT claims to validate.</param>
        /// <param name="isTokenValid">Validation flag; set to false if claims are null.</param>
        private void ValidateResetTokenClaims(
            (Guid userId, string email)? claims,
            ref bool isTokenValid)
        {
            if (claims == null)
            {
                isTokenValid = false;
            }
        }

        /// <summary>
        /// Validates whether the retrieved user exists and their Id matches the JWT subject claim.
        /// </summary>
        /// <param name="user">The user entity resolved from the email claim.</param>
        /// <param name="claims">The decoded JWT claims containing the userId to match against.</param>
        /// <param name="isUserValid">Validation flag; set to false if the user is null or Id mismatches.</param>
        private void ValidateUserMatchesToken(
            User? user,
            (Guid userId, string email)? claims,
            ref bool isUserValid)
        {
            if (user == null || claims == null || user.Id != claims.Value.userId)
            {
                isUserValid = false;
            }
        }

        /// <summary>
        /// Executes the password hash update when all validation flags are valid.
        /// </summary>
        /// <param name="user">The user whose password will be updated.</param>
        /// <param name="newPassword">The plain-text new password to be hashed and stored.</param>
        /// <param name="isTokenValid">Flag indicating whether the token is valid.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid.</param>
        private async Task ExecuteUpdatePassword(
            User? user,
            string newPassword,
            bool isTokenValid,
            bool isUserValid)
        {
            if (!isTokenValid || !isUserValid || user == null)
            {
                return;
            }
            var passwordHash = PasswordHelper.HashPassword(newPassword);
            await _updatePasswordHash.Execute(user, passwordHash);
        }

        /// <summary>
        /// Executes sending of a security notification email when all validation flags are valid.
        /// </summary>
        /// <param name="user">The user to notify about the password change.</param>
        /// <param name="isTokenValid">Flag indicating whether the token is valid.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid.</param>
        private async Task ExecuteSendNotification(
            User? user,
            bool isTokenValid,
            bool isUserValid)
        {
            if (!isTokenValid || !isUserValid || user == null)
            {
                return;
            }
            await _emailVerifyService.SendPasswordChangedNotificationAsync(
                user.Email, user.FullName);
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
        /// <param name="isTokenValid">Flag indicating whether the reset token is valid.</param>
        /// <param name="isUserValid">Flag indicating whether the user is valid.</param>
        /// <returns>
        /// Success response if all flags are valid; otherwise a failure response
        /// with the corresponding error code.
        /// </returns>
        private ApiResponse<ResetPasswordResponse> CreateResponse(
            ResetPasswordResponse response,
            bool isTokenValid,
            bool isUserValid)
        {
            if (!isTokenValid)
                return ApiResponse<ResetPasswordResponse>.Fail(
                    MessageCode.APP_MESSAGE_4019.ToString());
            if (!isUserValid)
                return ApiResponse<ResetPasswordResponse>.Fail(
                    MessageCode.APP_MESSAGE_4020.ToString());
            return ApiResponse<ResetPasswordResponse>.Success(
                MessageCode.APP_MESSAGE_2000.ToString(), response);
        }
    }
}
