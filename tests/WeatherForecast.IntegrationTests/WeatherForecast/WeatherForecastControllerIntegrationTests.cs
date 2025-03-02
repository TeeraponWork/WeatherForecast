using Newtonsoft.Json;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.IntegrationTests.WeatherForecast
{
    public class WeatherForecastControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public WeatherForecastControllerIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsOkResult()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");

            // Assert
            response.EnsureSuccessStatusCode();  // ตรวจสอบว่า StatusCode เป็น 200 OK
        }

        [Fact]
        public async Task GetWeatherForecast_ReturnsCorrectData()
        {
            // Act
            var response = await _client.GetAsync("/WeatherForecast");
            var content = await response.Content.ReadAsStringAsync();
            var weatherForecasts = JsonConvert.DeserializeObject<List<WeatherForecasts>>(content);

            // Assert
            Assert.NotNull(weatherForecasts);
            Assert.Equal(5, weatherForecasts.Count); // คาดว่าผลลัพธ์ควรจะมี 5 รายการ
        }
    }
}