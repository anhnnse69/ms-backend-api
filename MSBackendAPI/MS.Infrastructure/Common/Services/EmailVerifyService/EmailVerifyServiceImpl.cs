using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MS.Domain.Enums.GeneralCodes;
using System.Net;

namespace MS.Infrastructure.Common.Services.EmailVerifyService
{
    /// <summary>
    /// Provides email sending functionality for password reset and security notification workflows.
    /// Reads SMTP configuration from appsettings and sends HTML-formatted emails via MailKit.
    /// </summary>
    public class EmailVerifyServiceImpl : IEmailVerifyService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailVerifyServiceImpl"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration used to read SMTP settings.</param>
        public EmailVerifyServiceImpl(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Sends a 6-digit OTP code to the specified user for password reset verification.
        /// Throws an exception with <see cref="MessageCode.APP_MESSAGE_5003"/> code if the send operation fails.
        /// </summary>
        /// <param name="toEmail">The recipient email address.</param>
        /// <param name="fullName">The full name of the recipient used in the email body.</param>
        /// <param name="otpCode">The 6-digit OTP code to include in the email.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        public async Task SendOtpEmailAsync(string toEmail, string fullName, string otpCode)
        {
            var subject = BuildOtpSubject();
            var body = BuildOtpEmailBody(fullName, otpCode);
            await SendEmailAsync(toEmail, fullName, subject, body);
        }

        /// <summary>
        /// Builds the email subject line for an OTP password reset email.
        /// </summary>
        /// <returns>The subject string for the OTP email.</returns>
        private string BuildOtpSubject()
        {
            return "[MS] Your Password Reset OTP Code";
        }

        /// <summary>
        /// Builds the HTML email body containing the 6-digit OTP code.
        /// </summary>
        /// <param name="fullName">The recipient's full name to personalize the message.</param>
        /// <param name="otpCode">The 6-digit OTP code to embed in the email.</param>
        /// <returns>An HTML string representing the OTP email body.</returns>
        private string BuildOtpEmailBody(string fullName, string otpCode)
        {
            var encodedFullName = WebUtility.HtmlEncode(fullName ?? string.Empty);
            var encodedOtpCode = WebUtility.HtmlEncode(otpCode ?? string.Empty);

            return $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"" />
  <style>
    body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
    .container {{ max-width: 600px; margin: 40px auto; background: #ffffff; border-radius: 8px; padding: 32px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
    .header {{ font-size: 22px; font-weight: bold; color: #2c3e50; margin-bottom: 16px; }}
    .body-text {{ font-size: 15px; color: #444444; line-height: 1.6; }}
    .otp-box {{ margin: 24px 0; text-align: center; font-size: 36px; font-weight: bold; letter-spacing: 12px; color: #3498db; background: #eaf4fb; padding: 20px; border-radius: 8px; }}
    .warning {{ margin-top: 16px; font-size: 13px; color: #e74c3c; }}
    .footer {{ margin-top: 32px; font-size: 12px; color: #aaaaaa; }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">Password Reset OTP</div>
    <div class=""body-text"">
      Hello <strong>{encodedFullName}</strong>,<br /><br />
      Use the OTP code below to reset your password.
      This code will expire in <strong>5 minutes</strong>.
    </div>
    <div class=""otp-box"">{encodedOtpCode}</div>
    <div class=""warning"">
      If you did not request a password reset, please ignore this email.
      Your password will not be changed.
    </div>
    <div class=""footer"">
      This is an automated message from the MS Medical Scheduling System.
      Please do not reply to this email.
    </div>
  </div>
</body>
</html>";
        }

        /// <summary>
        /// Sends a password reset email containing a reset link to the specified user.
        /// Throws an exception with <see cref="MessageCode.APP_MESSAGE_5003"/> code if the send operation fails.
        /// </summary>
        /// <param name="toEmail">The recipient email address.</param>
        /// <param name="fullName">The full name of the recipient used in the email body.</param>
        /// <param name="resetLink">The password reset link to include in the email.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        public async Task SendPasswordResetEmailAsync(string toEmail, string fullName, string resetLink)
        {
            var subject = BuildPasswordResetSubject();
            var body = BuildPasswordResetBody(fullName, resetLink);
            await SendEmailAsync(toEmail, fullName, subject, body);
        }

        /// <summary>
        /// Sends a security notification email informing the user that their password was changed.
        /// Throws an exception with <see cref="MessageCode.APP_MESSAGE_5003"/> code if the send operation fails.
        /// </summary>
        /// <param name="toEmail">The recipient email address.</param>
        /// <param name="fullName">The full name of the recipient used in the email body.</param>
        /// <returns>A task representing the asynchronous send operation.</returns>
        public async Task SendPasswordChangedNotificationAsync(string toEmail, string fullName)
        {
            var subject = BuildPasswordChangedSubject();
            var body = BuildPasswordChangedBody(fullName);
            await SendEmailAsync(toEmail, fullName, subject, body);
        }

        /// <summary>
        /// Builds the email subject line for a password reset email.
        /// </summary>
        /// <returns>The subject string for the password reset email.</returns>
        private string BuildPasswordResetSubject()
        {
            return "[MS] Password Reset Request";
        }

        /// <summary>
        /// Builds the email subject line for a password changed notification email.
        /// </summary>
        /// <returns>The subject string for the password changed notification email.</returns>
        private string BuildPasswordChangedSubject()
        {
            return "[MS] Your Password Has Been Changed";
        }

        /// <summary>
        /// Builds the HTML email body for a password reset request.
        /// </summary>
        /// <param name="fullName">The recipient's full name to personalize the message.</param>
        /// <param name="resetLink">The password reset URL to embed in the email.</param>
        /// <returns>An HTML string representing the email body.</returns>
        private string BuildPasswordResetBody(string fullName, string resetLink)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"" />
  <style>
    body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
    .container {{ max-width: 600px; margin: 40px auto; background: #ffffff; border-radius: 8px; padding: 32px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
    .header {{ font-size: 22px; font-weight: bold; color: #2c3e50; margin-bottom: 16px; }}
    .body-text {{ font-size: 15px; color: #444444; line-height: 1.6; }}
    .btn {{ display: inline-block; margin-top: 24px; padding: 12px 28px; background-color: #3498db; color: #ffffff; text-decoration: none; border-radius: 6px; font-size: 15px; font-weight: bold; }}
    .footer {{ margin-top: 32px; font-size: 12px; color: #aaaaaa; }}
    .warning {{ margin-top: 16px; font-size: 13px; color: #e74c3c; }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">Password Reset Request</div>
    <div class=""body-text"">
      Hello <strong>{fullName}</strong>,<br /><br />
      We received a request to reset the password for your account.
      Click the button below to set a new password. This link will expire in <strong>30 minutes</strong>.
    </div>
    <a class=""btn"" href=""{resetLink}"">Reset My Password</a>
    <div class=""warning"">
      If you did not request a password reset, please ignore this email.
      Your password will not be changed.
    </div>
    <div class=""footer"">
      This is an automated message from the MS Medical Scheduling System.
      Please do not reply to this email.
    </div>
  </div>
</body>
</html>";
        }

        /// <summary>
        /// Builds the HTML email body for a password changed security notification.
        /// </summary>
        /// <param name="fullName">The recipient's full name to personalize the message.</param>
        /// <returns>An HTML string representing the email body.</returns>
        private string BuildPasswordChangedBody(string fullName)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
  <meta charset=""UTF-8"" />
  <style>
    body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }}
    .container {{ max-width: 600px; margin: 40px auto; background: #ffffff; border-radius: 8px; padding: 32px; box-shadow: 0 2px 8px rgba(0,0,0,0.08); }}
    .header {{ font-size: 22px; font-weight: bold; color: #2c3e50; margin-bottom: 16px; }}
    .body-text {{ font-size: 15px; color: #444444; line-height: 1.6; }}
    .alert-box {{ margin-top: 20px; padding: 14px 18px; background-color: #fef9e7; border-left: 4px solid #f39c12; border-radius: 4px; font-size: 14px; color: #7d6608; }}
    .footer {{ margin-top: 32px; font-size: 12px; color: #aaaaaa; }}
  </style>
</head>
<body>
  <div class=""container"">
    <div class=""header"">Your Password Has Been Changed</div>
    <div class=""body-text"">
      Hello <strong>{fullName}</strong>,<br /><br />
      This is a confirmation that the password for your MS account has been successfully changed.
    </div>
    <div class=""alert-box"">
      If you did not make this change, please contact our support team immediately
      or request another password reset to secure your account.
    </div>
    <div class=""footer"">
      This is an automated security notification from the MS Medical Scheduling System.
      Please do not reply to this email.
    </div>
  </div>
</body>
</html>";
        }

        /// <summary>
        /// Constructs and sends an email message using SMTP settings from configuration.
        /// Throws an <see cref="InvalidOperationException"/> with <see cref="MessageCode.APP_MESSAGE_5003"/>
        /// as the message if the SMTP operation fails.
        /// </summary>
        /// <param name="toEmail">The destination email address.</param>
        /// <param name="toFullName">The recipient's display name.</param>
        /// <param name="subject">The email subject line.</param>
        /// <param name="htmlBody">The HTML content of the email body.</param>
        /// <returns>A task representing the asynchronous SMTP send operation.</returns>
        private async Task SendEmailAsync(string toEmail, string toFullName, string subject, string htmlBody)
        {
            try
            {
                var smtpSettings = _configuration.GetSection("Smtp");
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(
                    smtpSettings["SenderName"],
                    smtpSettings["SenderEmail"]));
                message.To.Add(new MailboxAddress(toFullName, toEmail));
                message.Subject = subject;
                message.Body = new TextPart("html") { Text = htmlBody };
                using var client = new SmtpClient();
                await client.ConnectAsync(
                    smtpSettings["Host"],
                    int.Parse(smtpSettings["Port"]!),
                    SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(
                    smtpSettings["Username"],
                    smtpSettings["Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    MessageCode.APP_MESSAGE_5003.ToString(), ex);
            }
        }
    }
}
