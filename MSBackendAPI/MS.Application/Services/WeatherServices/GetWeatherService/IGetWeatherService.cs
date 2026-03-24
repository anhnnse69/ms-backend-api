using MS.Application.Common.Response;

namespace MS.Application.Services.WeatherServices.GetWeatherService
{
    /// <summary>
    /// Interface for weather service
    /// </summary>
    public interface IGetWeatherService
    {
        /// <summary>
        /// Process get weather request
        /// </summary>
        /// <param name="request">Weather request</param>
        /// <returns>Weather response</returns>
        Task<ApiResponse<GetWeatherResponse>> Process(GetWeatherRequest request);
    }
}
