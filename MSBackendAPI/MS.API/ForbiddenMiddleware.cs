using MS.Domain.Enums.GeneralCodes;
using System.Text.Json;

namespace MS.API
{
    /// <summary>
    /// Middleware that intercepts HTTP 403 Forbidden responses and writes a standardized JSON error message to the
    /// response body.
    /// </summary>
    /// <remarks>This middleware should be registered in the ASP.NET Core request pipeline to ensure that
    /// clients receive a consistent JSON payload when access is denied. It is typically used in APIs to provide
    /// machine-readable error information for forbidden requests. The middleware does not prevent 403 responses from
    /// being generated; it only modifies the response body when such a status code is detected.</remarks>
    public class ForbiddenMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the ForbiddenMiddleware class with the specified request delegate.
        /// </summary>
        /// <param name="next">The next middleware component in the HTTP request pipeline. Cannot be null.</param>
        public ForbiddenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Processes an HTTP request and, if the response status code is 403 Forbidden, writes a standardized JSON
        /// error message to the response body.
        /// </summary>
        /// <remarks>If the response status code is set to 403 Forbidden by downstream middleware, this
        /// method sets the response content type to "application/json" and writes a JSON object containing an
        /// application-specific error code. This allows clients to receive a consistent error format for forbidden
        /// requests.</remarks>
        /// <param name="context">The HTTP context for the current request. Provides access to request and response information.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    codeMessage = AuthMessageCode.APP_MESSAGE_0003.ToString(),
                }));
            }
        }
    }
}
