using WeatherForecast.Application.Core.Results;
using WeatherForecast.Domain.Entities;
namespace WeatherForecast.Application.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<Result<List<WeatherForecasts>>> GetWeatherForecasts();
    }
}
