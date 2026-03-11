using MS.Application.Common.Response;
using MS.Domain.Enums.GeneralCodes;
using System.Text.Json;

namespace MS.API
{
    /// <summary>
    /// Middleware that intercepts HTTP 403 Forbidden responses and writes a standardized
    /// JSON error body using the <see cref="ApiResponse{T}"/> wrapper.
    /// </summary>
    /// <remarks>
    /// This middleware should be registered in the ASP.NET Core request pipeline to ensure
    /// that clients receive a consistent JSON payload when access is denied. It is typically
    /// used in APIs to provide machine-readable error information for forbidden requests.
    /// The middleware does not prevent 403 responses from being generated; it only intercepts
    /// and rewrites the response body when such a status code is detected.
    /// </remarks>
    public class ForbiddenMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ForbiddenMiddleware"/> class.
        /// </summary>
        /// <param name="next">
        /// The next middleware component in the HTTP request pipeline. Cannot be null.
        /// </param>
        public ForbiddenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Processes an HTTP request and, if the response status code is 403 Forbidden,
        /// overwrites the response body with a standardized <see cref="ApiResponse{T}"/> payload.
        /// </summary>
        /// <remarks>
        /// The response is serialized using camelCase property naming to match the API convention.
        /// The error code returned is <see cref="AuthMessageCode.APP_MESSAGE_0003"/>,
        /// indicating the user does not have permission to access the requested resource.
        /// </remarks>
        /// <param name="context">
        /// The HTTP context for the current request. Provides access to request and response information.
        /// </param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
            {
                context.Response.ContentType = "application/json";
                var response = ApiResponse<object>.Fail(
                    AuthMessageCode.APP_MESSAGE_0003.ToString()
                );
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