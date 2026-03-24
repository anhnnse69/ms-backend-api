namespace MS.Application.Services.WeatherServices.GetWeatherService
{
    /// <summary>
    /// Response model for weather information
    /// </summary>
    public class GetWeatherResponse
    {
        // Temperature in Celsius
        public double Temperature { get; set; }
        // Weather condition (Rain, Clear, Clouds...)
        public string Condition { get; set; }
        // Weather description
        public string Description { get; set; }
        // Humidity percentage
        public int Humidity { get; set; }
        // Custom advice
        public string Advice { get; set; }
    }
}
