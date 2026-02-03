using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text;

namespace MS.API.Helpers
{
    /// <summary>
    /// ModelState helper methods for extracting and formatting validation errors.
    /// </summary>
    public static class ModelStateHelper
    {
        /// <summary>
        /// Returns a dictionary of field → list of error messages.
        /// </summary>
        public static Dictionary<string, List<string>> GetFieldErrors(ModelStateDictionary modelState)
        {
            return modelState
                .Where(entry => entry.Value.Errors.Any())
                .ToDictionary(
                    entry => entry.Key,
                    entry => entry.Value.Errors
                                    .Select(e => string.IsNullOrEmpty(e.ErrorMessage)
                                        ? e.Exception?.Message
                                        : e.ErrorMessage)
                                    .ToList()
                );
        }

        /// <summary>
        /// Returns a flat list of all error messages.
        /// </summary>
        public static List<string> GetAllMessages(ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => string.IsNullOrEmpty(e.ErrorMessage)
                    ? e.Exception?.Message
                    : e.ErrorMessage)
                .ToList();
        }

        /// <summary>
        /// Returns a formatted string for logging.
        /// </summary>
        public static string FormatErrors(string messageCode, ModelStateDictionary modelState)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Request log\nSystem message: {messageCode}");

            var fieldLines = modelState
                .Where(entry => entry.Value.Errors.Any())
                .Select(entry =>
                {
                    var codes = entry.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return $"{entry.Key}: {string.Join(", ", codes)}";
                });

            foreach (var line in fieldLines)
            {
                builder.AppendLine(line);
            }

            return builder.ToString().TrimEnd();
        }
    }
}
