namespace MS.Application.Services.PatientServices.ChangePasswordService
{
    /// <summary>
    /// Response model for change password
    /// </summary>
    public class ChangePasswordResponse
    {
        /// <summary>
        /// Success message
        /// </summary>
        public string Message { get; set; }

        public ChangePasswordResponse(string message)
        {
            Message = message;
        }
    }
}