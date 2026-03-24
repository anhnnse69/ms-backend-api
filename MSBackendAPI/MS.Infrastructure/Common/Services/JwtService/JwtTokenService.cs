using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MS.Domain.Entities;

namespace MS.Infrastructure.Common.Services.JwtService
{
    /// <summary>
    /// Provides functionality for generating JSON Web Tokens (JWT) for authenticated users.
    /// </summary>
    /// <remarks>This service retrieves JWT configuration settings from the application's configuration
    /// provider. It is typically used to issue tokens for user authentication and authorization in web applications.
    /// The generated tokens include user identity and role claims, and are signed using the configured secret
    /// key.</remarks>
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the JwtTokenService class using the specified configuration settings.
        /// </summary>
        /// <param name="configuration">The configuration settings used to initialize the service. Cannot be null.</param>
        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a JSON Web Token (JWT) for the specified user and outputs the token's expiration time.
        /// </summary>
        /// <remarks>The expiration time is determined by the 'ExpireHours' setting in the application's
        /// configuration. The token includes the user's ID and role as claims. The caller is responsible for securely
        /// storing and transmitting the token.</remarks>
        /// <param name="user">The user for whom the JWT will be generated. The user's identifier and role are included as claims in the
        /// token.</param>
        /// <returns>A string containing the generated JWT. The token can be used for authenticating the specified user until it
        /// expires.</returns>
        public string GenerateToken(User user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            // 1. Set expired time (24h)
            var expiredAt = DateTime.UtcNow.AddHours(
                double.Parse(jwtSettings["ExpireHours"]!)
            );
            // 2. Create claims
            var claims = new List<Claim>
            {
                // User identifier
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.Id.ToString()),

                // Role
                new Claim(ClaimTypes.Role, user.Role.ToString()),

                // Basic profile information for frontend display
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim("UserName", user.Username ?? string.Empty),
                new Claim("FullName", user.FullName ?? string.Empty),
                new Claim("DisplayName", user.DisplayName ?? string.Empty),

                // Token id
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            // 3. Create signing key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)
            );
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );
            // 4. Create JWT token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expiredAt,
                signingCredentials: credentials
            );
            // 5. Return token string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
