using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MS.API
{
    /// <summary>
    /// Middleware that intercepts ValidationException thrown by the MediatR
    /// DataAnnotationValidationBehavior pipeline and returns a standardized
    /// JSON 400 response using the ApiResponse wrapper.
    /// </summary>
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationExceptionMiddleware> _logger;

        public ValidationExceptionMiddleware(
            RequestDelegate next,
            ILogger<ValidationExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning("Validation failed: {Errors}", ex.Message);

                var response = new
                {
                    CodeMessage = MessageCode.APP_MESSAGE_4019.ToString(),
                    Data = (object)null,
                    Meta = (object)null,
                    Errors = ex.Message
                                   .Split(';', StringSplitOptions.RemoveEmptyEntries)
                                   .Select(e => e.Trim())
                                   .ToList()
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        response,
                        new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
                    )
                );
            }
        }
    }
}