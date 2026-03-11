using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MS.Application.Common.Attributes
{
    /// <summary>
    /// Not Blank validation attribute
    /// </summary>
    public class NotBlankAttribute : ValidationAttribute
    {
        private const string GeneralPattern = @"^\S+(?: \S+)*$";

        /// <summary>
        /// Validates that the specified value is not null, and if it is a string, that it is not empty or consists only
        /// of white-space characters.
        /// </summary>
        /// <param name="value">The value of the object to validate. If the value is a string, it must not be null, empty, or white space.</param>
        /// <param name="validationContext">The context information about the validation operation, including the object and member being validated.</param>
        /// <returns>A ValidationResult that indicates whether the value is valid. Returns ValidationResult.Success if the value
        /// is valid; otherwise, a ValidationResult with the error message.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var message = ErrorMessage;
            if (value is string str)
            {
                // Check blank
                if (string.IsNullOrWhiteSpace(str))
                    return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
                // Check regex
                if (!Regex.IsMatch(str, GeneralPattern))
                    return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            }
            return ValidationResult.Success;
        }
    }
}
