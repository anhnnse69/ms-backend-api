using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MS.Application.Common.Attributes
{
    /// <summary>
    /// Validates that the input does not contain SQL or NoSQL injection patterns.
    /// Protects against database manipulation attacks.
    /// </summary>
    /// <remarks>
    /// Blocks patterns such as:
    /// - SQL keywords used in injection: OR 1=1, DROP TABLE, UNION SELECT, etc.
    /// - Comment sequences used to truncate queries: --, #, /* */
    /// - Stacked queries via semicolons: ; DROP TABLE
    /// - NoSQL injection operators: $where, $gt, $ne, $or, etc.
    /// - Encoded payloads: %27 (single quote), %3B (semicolon), %2D%2D (--)
    /// </remarks>
    public class NoSqlInjectionAttribute : ValidationAttribute
    {
        // Common SQL injection keywords and clause patterns
        private const string SqlKeywordPattern =
            @"(?i)\b(SELECT|INSERT|UPDATE|DELETE|DROP|ALTER|CREATE|TRUNCATE|EXEC|EXECUTE|UNION|FROM|WHERE|HAVING|GROUP\s+BY|ORDER\s+BY|CAST|CONVERT|DECLARE|WAITFOR|XP_)\b";

        // SQL comment sequences used to truncate or bypass queries
        private const string SqlCommentPattern = @"(--|#|/\*|\*/)";

        // Tautology patterns like OR 1=1, AND 1=1, OR 'a'='a'
        private const string TautologyPattern =
            "(?i)(\\bOR\\b|\\bAND\\b)\\s+(['\"][^'\"]*['\"]\\s*=\\s*['\"][^'\"]*['\"]|\\d+\\s*=\\s*\\d+)";

        // NoSQL injection operators ($where, $gt, $ne, $or, ...)
        private const string NoSqlOperatorPattern = @"\$\w+";

        // URL-encoded dangerous characters: %27=', %3B=;, %2D%2D=--
        private const string UrlEncodedPattern = @"(%27|%3B|%2D%2D|%23|%2F\*|%2A%2F)";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = ErrorMessage;
            if (value is not string input)
                return ValidationResult.Success;
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Success;
            if (Regex.IsMatch(input, SqlKeywordPattern) ||
                Regex.IsMatch(input, SqlCommentPattern) ||
                Regex.IsMatch(input, TautologyPattern) ||
                Regex.IsMatch(input, NoSqlOperatorPattern) ||
                Regex.IsMatch(input, UrlEncodedPattern, RegexOptions.IgnoreCase))
            {
                return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            }
            return ValidationResult.Success;
        }
    }
}