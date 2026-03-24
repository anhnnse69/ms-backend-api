using System.Text.Json.Serialization;

public class WeatherDto
{
    [JsonPropertyName("main")]
    public MainInfo Main { get; set; }

    [JsonPropertyName("weather")]
    public List<WeatherInfo> Weather { get; set; }
}

public class MainInfo
{
    [JsonPropertyName("temp")]
    public double Temp { get; set; }

    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }
}

public class WeatherInfo
{
    [JsonPropertyName("main")]
    public string Main { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}