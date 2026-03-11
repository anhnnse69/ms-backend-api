using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MS.Application.Common.Attributes
{
    /// <summary>
    /// Validates that the input does not contain HTML tags or JavaScript injection patterns.
    /// Protects against Cross-Site Scripting (XSS) attacks.
    /// </summary>
    /// <remarks>
    /// Blocks patterns such as:
    /// - HTML tags: &lt;script&gt;, &lt;img&gt;, &lt;a href&gt;, &lt;iframe&gt;, etc.
    /// - Inline event handlers: onerror=, onclick=, onload=, etc.
    /// - JavaScript URIs: javascript:, vbscript:
    /// - HTML entities used to bypass filters: &amp;lt;, &amp;#60;, etc.
    /// </remarks>
    public class NoHtmlAttribute : ValidationAttribute
    {
        // Matches any HTML tag
        private const string HtmlTagPattern = @"<[^>]*>";

        // Matches inline JS event handlers (onclick=, onerror=, onload=, ...)
        private const string EventHandlerPattern = @"\bon\w+\s*=";

        // Matches javascript: and vbscript: URI schemes
        private const string DangerousUriPattern = @"(?i)(javascript|vbscript)\s*:";

        // Matches HTML-encoded angle brackets used to bypass tag filters
        private const string EncodedTagPattern = @"(&lt;|&gt;|&#60;|&#62;|&#x3C;|&#x3E;)";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = ErrorMessage;
            if (value is not string input)
                return ValidationResult.Success;
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Success;
            if (Regex.IsMatch(input, HtmlTagPattern, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(input, EventHandlerPattern, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(input, DangerousUriPattern, RegexOptions.IgnoreCase) ||
                Regex.IsMatch(input, EncodedTagPattern, RegexOptions.IgnoreCase))
            {
                return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            }
            return ValidationResult.Success;
        }
    }
}