using MS.Domain.Enums.Roles;

namespace MS.Application.Services.AdminServices.CreateUser
{
    /// <summary>
    /// Request object for create user.
    /// </summary>
    public class CreateUserRequest
    {
        /// The email address of the user.
        public string Email { get; set; }
        /// The password used for the user account.
        public string Password { get; set; }
        /// The full name of the user.
        public string FullName { get; set; }
        /// The avatar URL representing the user's profile image.
        public string AvatarUrl { get; set; }
        /// The phone number of the user.
        public string PhoneNumber { get; set; }
        /// The system role assigned to the user.
        public SystemRole Role { get; set; }
    }
}
