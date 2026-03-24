using System.Text.Json;

namespace MS.Infrastructure.ExternalServices.Weather
{
    /// <summary>
    /// Implementation for retrieving weather data from OpenWeather API
    /// </summary>
    public class GetWeatherExternalImpl : IGetWeatherExternal
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClient">Http client instance</param>
        public GetWeatherExternalImpl(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Execute API call to retrieve weather data
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Weather DTO</returns>
        public async Task<WeatherDto?> Execute(string city)
        {
            var encodedCity = Uri.EscapeDataString(city);
            var response = await _httpClient.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?q={encodedCity}&appid=ff43a8495f2c3fa72e90c15cf86bf4d7"
            );
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return JsonSerializer.Deserialize<WeatherDto>(
                content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
