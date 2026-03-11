namespace MS.Application.Services.AdminServices.GetAllUsersService
{
    /// <summary>
    /// Response model for getting all users
    /// </summary>
    public class GetAllUsersResponse
    {
        // User identifier
        public Guid Id { get; set; }

        // Username
        public string Username { get; set; }

        // Display name
        public string DisplayName { get; set; }

        // Full name
        public string FullName { get; set; }

        // Email
        public string Email { get; set; }

        // Phone number
        public string PhoneNumber { get; set; }

        // Avatar url
        public string AvatarUrl { get; set; }

        // User role
        public string Role { get; set; }

        // Deletion status (true = soft-deleted and no longer active, false = active)
        public bool IsDeleted { get; set; }
    }
}