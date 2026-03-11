using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MS.API.Helpers;
using MS.Domain.Enums.GeneralCodes;

namespace MS.API.Filters
{
    /// <summary>
    /// Action filter that validates ModelState before the action executes.
    /// Returns a standardized 400 ApiResponse when ModelState is invalid.
    /// Replaces the default behavior suppressed by SuppressModelStateInvalidFilter.
    /// </summary>
    public class ModelStateValidationFilter : IActionFilter
    {
        private readonly ILogger<ModelStateValidationFilter> _logger;

        public ModelStateValidationFilter(ILogger<ModelStateValidationFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid) return;

            var errors = ModelStateHelper.GetFieldErrors(context.ModelState);
            var logText = ModelStateHelper.FormatErrors(
                MessageCode.APP_MESSAGE_4019.ToString(),
                context.ModelState
            );

            _logger.LogWarning(logText);

            var response = new
            {
                CodeMessage = MessageCode.APP_MESSAGE_4019.ToString(),
                Data = (object)null,
                Meta = (object)null,
                Errors = errors
            };

            context.Result = new BadRequestObjectResult(response);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}