namespace MS.Infrastructure.ExternalServices.Weather
{
    /// <summary>
    /// Interface for retrieving weather data from external API
    /// </summary>
    public interface IGetWeatherExternal
    {
        /// <summary>
        /// Execute request to get weather information by city
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Weather DTO from external API</returns>
        Task<WeatherDto?> Execute(string city);
    }
}
