using MS.Domain.Entities.General;
using MS.Domain.Entities.General.Interfaces;

namespace MS.Domain.Entities
{
    /// <summary>
    /// Represents a password reset token issued to a user
    /// </summary>
    public class PasswordResetToken : EntityBase<Guid>, IEntityBase<Guid>
    {
        // User requesting password reset
        public Guid UserId { get; set; }
        public User? User { get; set; }

        // Random token for verification
        public string? Token { get; set; }

        // Token expiration time
        public DateTimeOffset ExpiresAt { get; set; }

        // Whether the token has been used
        public bool IsUsed { get; set; } = false;

        // When the token was used (if applicable)
        public DateTimeOffset? UsedAt { get; set; }

        // Creation timestamp
        public DateTimeOffset CreatedAt { get; set; }
    }
}
