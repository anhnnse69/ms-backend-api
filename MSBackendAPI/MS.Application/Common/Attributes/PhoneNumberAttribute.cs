using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MS.Application.Common.Attributes
{
    /// <summary>
    /// Validates Vietnamese phone number format.
    /// Accepts formats: 0xxxxxxxxx or +84xxxxxxxxx (9-10 digits after prefix).
    /// </summary>
    public class PhoneNumberAttribute : ValidationAttribute
    {
        private const string PhonePattern = @"^(0|\+84)(3[2-9]|5[6-9]|7[0|6-9]|8[0-9]|9[0-9])\d{7}$";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var message = ErrorMessage;
            if (value is not string phone)
                return ValidationResult.Success;
            if (string.IsNullOrWhiteSpace(phone))
                return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            if (!Regex.IsMatch(phone, PhonePattern))
                return new ValidationResult(message, validationContext?.MemberName != null ? [validationContext.MemberName] : null);
            return ValidationResult.Success;
        }
    }
}