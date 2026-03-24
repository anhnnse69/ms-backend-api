using MS.Application.Common.Response;
using MS.Domain.Enums.GeneralCodes;
using MS.Infrastructure.ExternalServices.Weather;

namespace MS.Application.Services.WeatherServices.GetWeatherService
{
    /// <summary>
    /// Service responsible for retrieving weather information
    /// </summary>
    public class GetWeatherService : IGetWeatherService
    {
        private readonly IGetWeatherExternal _getWeatherExternal;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="getWeatherExternal">External weather service</param>
        public GetWeatherService(IGetWeatherExternal getWeatherExternal)
        {
            _getWeatherExternal = getWeatherExternal;
        }

        /// <summary>
        /// Process get weather request
        /// </summary>
        /// <param name="request">Weather request</param>
        /// <returns>Weather response</returns>
        public async Task<ApiResponse<GetWeatherResponse>> Process(GetWeatherRequest request)
        {
            // 1. Initialize validation flags
            bool isDataValid = true;
            // 2. Retrieve weather data
            var retrievedWeather = await RetrieveWeather(request.City);
            // 3. Validate retrieved data
            ValidateData(retrievedWeather, ref isDataValid);
            // 4. Create response
            return CreateResponse(retrievedWeather, isDataValid);
        }

        /// <summary>
        /// Retrieve weather data from external service
        /// </summary>
        /// <param name="city">City name</param>
        /// <returns>Weather DTO</returns>
        private async Task<WeatherDto?> RetrieveWeather(string city)
        {
            return await _getWeatherExternal.Execute(city);
        }

        /// <summary>
        /// Validate weather data
        /// </summary>
        /// <param name="data">Weather DTO</param>
        /// <param name="isDataValid">Validation flag</param>
        private void ValidateData(WeatherDto? data, ref bool isDataValid)
        {
            if (data == null ||
                data.Main == null ||
                data.Weather == null ||
                !data.Weather.Any())
            {
                isDataValid = false;
            }
        }

        /// <summary>
        /// Map weather DTO to response model
        /// </summary>
        /// <param name="dto">Weather DTO</param>
        /// <returns>Weather response</returns>
        private GetWeatherResponse MapToResponse(WeatherDto dto)
        {
            return new GetWeatherResponse
            {
                // Convert Kelvin to Celsius
                Temperature = Math.Round(dto.Main.Temp - 273.15, 1),
                // Weather condition
                Condition = dto.Weather.FirstOrDefault()?.Main,
                // Weather description
                Description = dto.Weather.FirstOrDefault()?.Description,
                // Humidity percentage
                Humidity = dto.Main.Humidity,
                // Custom advice
                Advice = GenerateAdvice(dto)
            };
        }

        /// <summary>
        /// Generate weather-based advice
        /// </summary>
        /// <param name="dto">Weather DTO</param>
        /// <returns>Advice string</returns>
        private string GenerateAdvice(WeatherDto dto)
        {
            var condition = dto.Weather.FirstOrDefault()?.Main;
            var tempC = Math.Round(dto.Main.Temp - 273.15, 1);
            string tempAdvice = "";
            string weatherAdvice = "";

            // 1. Xử lý nhiệt độ
            if (tempC > 35)
            {
                tempAdvice = "Trời hôm nay khá nóng";
            }
            else if (tempC >= 30)
            {
                tempAdvice = "Trời hơi nóng";
            }
            else if (tempC <= 10)
            {
                tempAdvice = "Trời lạnh";
            }
            else
            {
                tempAdvice = "Thời tiết dễ chịu";
            }

            // 2. Xử lý điều kiện thời tiết, kết hợp với nhiệt độ vừa tạo
            switch (condition)
            {
                case "Rain":
                    weatherAdvice = "và có mưa, nhớ mang theo ô hoặc áo mưa khi đi hẹn";
                    break;
                case "Snow":
                    weatherAdvice = "và có tuyết, đi cẩn thận và dành thêm chút thời gian di chuyển";
                    break;
                case "Thunderstorm":
                    weatherAdvice = "với giông bão, cân nhắc đi sớm hoặc theo dõi dự báo";
                    break;
                case "Clouds":
                    if (tempC <= 30 && tempC >= 10)
                        weatherAdvice = "và nhiều mây, khá dễ chịu cho việc đi hẹn hôm nay";
                    break;
                case "Clear":
                    if (tempC <= 30 && tempC >= 10)
                        weatherAdvice = "với trời quang đãng, bạn có thể yên tâm đi hẹn hôm nay";
                    break;
                default:
                    weatherAdvice = "và thời tiết thay đổi, nhớ theo dõi dự báo trước khi đi hẹn";
                    break;
            }

            // 3. Ghép lại câu hoàn chỉnh
            string advice = string.IsNullOrEmpty(weatherAdvice) ? tempAdvice : $"{tempAdvice} {weatherAdvice}.";

            // 4. Thêm gợi ý riêng cho trời nóng hoặc lạnh
            if (tempC > 30 && tempC <= 35)
            {
                advice += " Mang theo nước uống để thoải mái hơn.";
            }
            else if (tempC > 35)
            {
                advice += " Nên tránh đặt lịch buổi trưa nhé.";
            }
            else if (tempC <= 10)
            {
                advice += " Nhớ mặc ấm trước khi đi hẹn.";
            }

            return advice;
        }

        /// <summary>
        /// Create API response
        /// </summary>
        /// <param name="data">Weather DTO</param>
        /// <param name="isDataValid">Validation flag</param>
        /// <returns>API response</returns>
        private ApiResponse<GetWeatherResponse> CreateResponse(
            WeatherDto? data,
            bool isDataValid)
        {
            if (!isDataValid)
            {
                return ApiResponse<GetWeatherResponse>
                    .Fail(MessageCode.APP_MESSAGE_5003.ToString());
            }
            var result = MapToResponse(data);
            return ApiResponse<GetWeatherResponse>
                .Success(MessageCode.APP_MESSAGE_2000.ToString(), result);
        }
    }
}
