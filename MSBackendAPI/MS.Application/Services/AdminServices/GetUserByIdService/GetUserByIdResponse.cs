namespace MS.Application.Services.AdminServices.GetUserByIdService
{
    /// <summary>
    /// Response model for admin get user by id
    /// </summary>
    public class GetUserByIdResponse
    {
        // User identifier
        public Guid Id { get; set; }

        // Username used for login
        public string Username { get; set; }

        // Display name shown in UI
        public string DisplayName { get; set; }

        // Full name of user
        public string FullName { get; set; }

        // Avatar URL
        public string AvatarUrl { get; set; }

        // Email address
        public string Email { get; set; }

        // Phone number
        public string PhoneNumber { get; set; }

        // System role
        public string Role { get; set; }

        // Indicates whether the account has been soft-deleted (true = deleted, false = active)
        public bool IsDeleted { get; set; }

    }
}