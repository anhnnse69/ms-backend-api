using MS.Domain.Enums.Roles;

namespace MS.Application.Services.AdminServices.UpdateUserService
{
    /// <summary>
    /// Represents the request data required to update a user.
    /// </summary>
    public class UpdateUserRequest
    {
        /// The username of the user.
        public string Username { get; set; } = string.Empty;
        /// The new password for the user (optional).
        public string PasswordHash { get; set; } = string.Empty;
        /// The full name of the user.
        public string FullName { get; set; } = string.Empty;
        /// The display name shown in the system.
        public string DisplayName { get; set; } = string.Empty;
        /// The avatar URL of the user.
        public string AvatarUrl { get; set; } = string.Empty;
        /// The email address of the user.
        public string Email { get; set; } = string.Empty;
        /// The phone number of the user.
        public string PhoneNumber { get; set; } = string.Empty;
        /// The system role assigned to the user.
        public SystemRole Role { get; set; }
        /// Indicates whether the user account is active.
        public bool IsDeleted { get; set; }
    }
}
