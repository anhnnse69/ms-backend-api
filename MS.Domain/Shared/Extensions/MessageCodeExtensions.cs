using MS.Domain.Enums.GeneralCodes;

namespace MS.Domain.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for the <see cref="MessageCode"/> enum to handle 
    /// code string conversion, HTTP status code mapping, and other utility operations.
    /// </summary>
    public static class MessageCodeExtensions
    {
        /// <summary>
        /// Converts the <see cref="MessageCode"/> enum value to its string representation 
        /// (e.g., "APP_MESSAGE_2001").
        /// </summary>
        /// <param name="code">The <see cref="MessageCode"/> enum value to convert.</param>
        /// <returns>The string name of the enum value (e.g., "APP_MESSAGE_2001").</returns>
        public static string ToCodeString(this MessageCode code)
        {
            return code.ToString();
        }

        /// <summary>
        /// Maps the <see cref="MessageCode"/> enum value to the corresponding HTTP status code 
        /// based on its prefix series:
        /// - 2000 series → 200 OK (Success)
        /// - 4000 series → 400 Bad Request (Client error)
        /// - 5000 series → 500 Internal Server Error (Server error)
        /// </summary>
        /// <param name="code">The <see cref="MessageCode"/> enum value to map.</param>
        /// <returns>The appropriate HTTP status code (defaults to 500 if no match).</returns>
        public static int ToHttpStatusCode(this MessageCode code)
        {
            var codeStr = code.ToString();

            // 2000 series → Success
            if (codeStr.StartsWith("APP_MESSAGE_2")) return 200;

            // 4000 series → Client error
            if (codeStr.StartsWith("APP_MESSAGE_4")) return 400;

            // 5000 series → Server error
            if (codeStr.StartsWith("APP_MESSAGE_5")) return 500;

            // Fallback for any unmapped codes
            return 500;
        }

        /// <summary>
        /// Returns the message code as a string in the format used by the application 
        /// (currently identical to <see cref="ToCodeString(MessageCode)"/>).
        /// This method is provided for future extensibility (e.g., custom formatting or prefixes).
        /// </summary>
        /// <param name="code">The <see cref="MessageCode"/> enum value.</param>
        /// <returns>The application message code string (e.g., "APP_MESSAGE_2001").</returns>
        public static string ToAppMessage(this MessageCode code)
        {
            return code.ToString();
        }
    }
}