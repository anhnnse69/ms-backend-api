using System;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Common.Attributes
{
    /// <summary>
    /// Validates that a Guid value is not empty (not equal to Guid.Empty).
    /// </summary>
    public class NotEmptyGuidAttribute : ValidationAttribute
    {
        /// <inheritdoc />
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;
            // Guid and Guid? both unbox to Guid if they have a value
            if (value is Guid guidValue)
            {
                if (guidValue == Guid.Empty)
                {
                    string[]? memberNames = validationContext?.MemberName != null ? [validationContext.MemberName] : null;
                    return new ValidationResult(ErrorMessage, memberNames);
                }
            }
            return ValidationResult.Success;
        }
    }
}