using MS.Application.Common.Attributes;
using MS.Domain.Enums.GeneralCodes;
using System.ComponentModel.DataAnnotations;

namespace MS.Application.Services.WeatherServices.GetWeatherService
{
    /// <summary>
    /// Request model for getting weather information of a city.
    /// </summary>
    public class GetWeatherRequest
    {
        /// <summary>
        /// Name of the city to get weather for.
        /// </summary>
        [Required(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NotBlank(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        [NoHtml(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4024))]
        [NoSqlInjection(ErrorMessage = nameof(MessageCode.APP_MESSAGE_4025))]
        [StringLength(100, MinimumLength = 2, ErrorMessage = nameof(MessageCode.APP_MESSAGE_4019))]
        public string City { get; set; } = string.Empty;
    }
}
