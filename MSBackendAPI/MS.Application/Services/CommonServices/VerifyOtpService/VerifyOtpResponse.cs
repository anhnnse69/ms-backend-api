namespace MS.Application.Services.CommonServices.VerifyOtpService
{
    /// <summary>
    /// Represents the response returned after successful OTP verification.
    /// </summary>
    public class VerifyOtpResponse
    {
        /// <summary>A short-lived JWT (15 minutes) scoped exclusively to authorize the reset-password step.</summary>
        public string ResetToken { get; set; }
    }
}
