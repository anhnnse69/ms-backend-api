namespace MS.Application.Services.PatientServices.GetUserProfileService
{
    /// <summary>
    /// Represents the personal profile information of an authenticated user.
    /// </summary>
    public class GetUserProfileResponse
    {
        /// <summary>The unique identifier of the user.</summary>
        public Guid Id { get; set; }
        /// <summary>The display name of the user.</summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>The full name of the user.</summary>
        public string FullName { get; set; } = string.Empty;
        /// <summary>The email address of the user.</summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>The phone number of the user.</summary>
        public string PhoneNumber { get; set; } = string.Empty;
        /// <summary>The avatar URL of the user.</summary>
        public string? AvatarUrl { get; set; }
    }
}
