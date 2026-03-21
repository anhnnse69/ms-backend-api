namespace MS.Infrastructure.Common.Services.JwtResetToken
{
    /// <summary>
    /// Defines a service for generating and validating short-lived scoped reset JWTs.
    /// These tokens are exclusively used to authorize the reset-password step.
    /// </summary>
    public interface IJwtResetTokenService
    {
        /// <summary>
        /// Generates a scoped reset JWT for the given user, valid for 15 minutes.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A signed JWT string scoped to password reset operations.</returns>
        string GenerateResetToken(Guid userId, string email);

        /// <summary>
        /// Validates the reset JWT and extracts the userId and email claims.
        /// </summary>
        /// <param name="token">The signed reset JWT string to validate.</param>
        /// <returns>A tuple containing userId and email if valid; otherwise null.</returns>
        (Guid userId, string email)? ValidateResetToken(string token);
    }
}
