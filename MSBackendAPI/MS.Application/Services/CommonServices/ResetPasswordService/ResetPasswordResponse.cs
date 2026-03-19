namespace MS.Application.Services.CommonServices.ResetPasswordService
{
    /// <summary>
    /// Represents the response returned after a password reset is successfully completed.
    /// </summary>
    public class ResetPasswordResponse
    {
        /// <summary>A message confirming the password has been successfully reset.</summary>
        public string Message { get; set; }
    }
}
