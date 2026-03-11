namespace MS.API
{
    /// <summary>
    /// Middleware that provides centralized exception handling for HTTP requests in the application pipeline.
    /// </summary>
    /// <remarks>This middleware intercepts unhandled exceptions thrown during request processing and returns
    /// a standardized JSON error response with HTTP status code 500 (Internal Server Error). It also logs the exception
    /// details using the configured logger. Place this middleware early in the pipeline to ensure that exceptions from
    /// subsequent middleware and request handlers are properly handled.</remarks>
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the GlobalExceptionMiddleware class with the specified request delegate and
        /// logger.
        /// </summary>
        /// <param name="next">The next middleware component in the HTTP request pipeline. Cannot be null.</param>
        /// <param name="logger">The logger used to record exception details. Cannot be null.</param>
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Processes an HTTP request and handles any unhandled exceptions by returning a standardized JSON error
        /// response with status code 500.
        /// </summary>
        /// <remarks>If an unhandled exception occurs during request processing, this method logs the
        /// error and returns a JSON response with a 500 status code. The response includes a code message and a generic
        /// error message, but does not expose exception details to the client.</remarks>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                var response = new
                {
                    CodeMessage = "APP_MESSAGE_5000",
                    Message = "Internal server error",
                    Data = (object)null,
                    Meta = (object)null
                };
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
