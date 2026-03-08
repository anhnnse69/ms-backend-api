using MS.Domain.Enums.Roles;

namespace MS.Application.Services.AdminServices.CreateUser
{
    /// <summary>
    /// Request object for create user.
    /// </summary>
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string AvatarUrl { get; set; }
        public string PhoneNumber { get; set; }
        public SystemRole Role { get; set; }
    }
}
