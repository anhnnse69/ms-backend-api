using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MS.Application.Common.Attributes
{
    /// <summary>
    /// Password Strength validation attribute
    /// </summary>
    public class PasswordStrengthAttribute : ValidationAttribute
    {
        private const string UppercasePattern = @"[A-Z]";
        private const string NumberPattern = @"\d";
        private const string SpecialCharPattern = @"^(?=.*[^a-zA-Z0-9]).+$";

        /// <summary>
        /// Validates that the specified value meets the password requirements for uppercase letters, numbers, and
        /// special characters.
        /// </summary>
        /// <remarks>If the value is not a string, the method considers it valid and returns
        /// ValidationResult.Success. The password must contain at least one uppercase letter, one number, and one
        /// special character to be considered valid.</remarks>
        /// <param name="value">The value of the object to validate. Expected to be a string representing the password to check.</param>
        /// <param name="validationContext">The context information about the validation operation, including the object and member being validated.</param>
        /// <returns>A ValidationResult that indicates whether the value is valid. Returns ValidationResult.Success if the value
        /// meets all password requirements; otherwise, returns a ValidationResult with the appropriate error message.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var message = ErrorMessage;
            if (value is not string password)
                return ValidationResult.Success;
            // At least one uppercase letter
            if (!Regex.IsMatch(password, UppercasePattern))
                return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            // At least one number
            if (!Regex.IsMatch(password, NumberPattern))
                return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            // At least one special character
            if (!Regex.IsMatch(password, SpecialCharPattern))
                return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            return ValidationResult.Success;
        }
    }
}
