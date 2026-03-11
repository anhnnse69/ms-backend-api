namespace MS.Application.Services.CommonServices.RegisterService
{
    /// <summary>
    /// Response object for patient registration.
    /// </summary>
    public class RegisterResponse
    {
        public string Email { get; set; }
        public string FullName { get; set; }

        public RegisterResponse(string email, string fullName)
        {
            Email = email;
            FullName = fullName;
        }
    }
}