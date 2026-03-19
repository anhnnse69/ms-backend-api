using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MS.Infrastructure.Common.Services.JwtResetToken
{
    /// <summary>
    /// Provides functionality for generating and validating short-lived scoped reset JWTs.
    /// Tokens are signed with the application secret and carry a password_reset scope claim
    /// to prevent misuse as general authentication tokens.
    /// </summary>
    public class JwtResetTokenService : IJwtResetTokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtResetTokenService"/> class.
        /// </summary>
        /// <param name="configuration">Application configuration used to read JWT settings.</param>
        public JwtResetTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a scoped reset JWT for the given user, valid for 15 minutes.
        /// The token includes userId, email, and a password_reset scope claim.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="email">The email address of the user.</param>
        /// <returns>A signed JWT string scoped to password reset operations.</returns>
        public string GenerateResetToken(Guid userId, string email)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim("scope", "password_reset"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Validates the reset JWT and extracts the userId and email claims.
        /// Returns null if the token is invalid, expired, or not scoped to password_reset.
        /// </summary>
        /// <param name="token">The signed reset JWT string to validate.</param>
        /// <returns>A tuple containing userId and email if valid; otherwise null.</returns>
        public (Guid userId, string email)? ValidateResetToken(string token)
        {
            try
            {
                var jwtSettings = _configuration.GetSection("Jwt");
                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
                var handler = new JwtSecurityTokenHandler();
                var principal = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out _);
                var scope = principal.FindFirst("scope")?.Value;
                if (scope != "password_reset")
                {
                    return null;
                }
                var userIdStr = principal.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
                var email = principal.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
                if (!Guid.TryParse(userIdStr, out var userId))
                {
                    return null;
                }
                if (string.IsNullOrWhiteSpace(email))
                {
                    return null;
                }
                return (userId, email);
            }
            catch
            {
                return null;
            }
        }
    }
}
