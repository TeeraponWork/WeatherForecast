using WeatherForecast.Application.Core.Results;
using WeatherForecast.Application.Interfaces;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", 
            "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public async Task<Result<List<WeatherForecasts>>> GetWeatherForecasts()
        {
            try
            {
                var result = Enumerable.Range(1, 5).Select(index => new WeatherForecasts
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToList();

                return Result<List<WeatherForecasts>>.Success(result.ToList());
            }
            catch (Exception ex)
            {
                return Result<List<WeatherForecasts>>.Failure(ex.Message);
            }          
        }
    }
}
