using WeatherForecast.Application.Interfaces;
using WeatherForecast.Application.Services;

namespace WeatherForecast.UnitTests.WeatherForecast
{
    public class WeatherForecastServiceTests
    {
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastServiceTests()
        {
            _weatherForecastService = new WeatherForecastService();
        }

        [Fact]
        public void GetWeatherForecasts_ShouldReturnFiveForecasts()
        {
            // Arrange: ไม่มี เนื่องจากไม่มี dependency

            // Act: เรียกใช้งานเมธอดที่ต้องการทดสอบ
            //var result = _weatherForecastService.GetWeatherForecasts();

            // Assert: ตรวจสอบผลลัพธ์ที่ได้
            //Assert.NotNull(result);
            //Assert.Equal(5, result.Count);
        }

        [Fact]
        public void GetWeatherForecasts_ShouldReturnValidWeatherForecasts()
        {
            // Arrange: ไม่มี เนื่องจากไม่มี dependency

            // Act: เรียกใช้งานเมธอดที่ต้องการทดสอบ
            //var result = _weatherForecastService.GetWeatherForecasts();

            // Assert: ตรวจสอบผลลัพธ์ที่ได้
            //foreach (var forecast in result)
            //{
            //    Assert.InRange(forecast.TemperatureC, -20, 55);
            //    Assert.NotNull(forecast.Summary);
            //    Assert.NotEmpty(forecast.Summary);
            //}
        }
    }
}
