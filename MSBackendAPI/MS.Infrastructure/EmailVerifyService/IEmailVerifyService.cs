namespace MS.Infrastructure.EmailVerifyService
{
    /// <summary>
    /// Defines the contract for the email sending service.
    /// </summary>
    public interface IEmailVerifyService
    {
        /// <summary>
        /// Sends a password reset email containing a reset link to the specified user.
        /// </summary>
        /// <param name="toEmail">The recipient email address.</param>
        /// <param name="fullName">The full name of the recipient used in the email body.</param>
        /// <param name="resetLink">The password reset link to include in the email.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        Task SendPasswordResetEmailAsync(string toEmail, string fullName, string resetLink);

        /// <summary>
        /// Sends a security notification email informing the user that their password was changed.
        /// </summary>
        /// <param name="toEmail">The recipient email address.</param>
        /// <param name="fullName">The full name of the recipient used in the email body.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        Task SendPasswordChangedNotificationAsync(string toEmail, string fullName);
    }
}
