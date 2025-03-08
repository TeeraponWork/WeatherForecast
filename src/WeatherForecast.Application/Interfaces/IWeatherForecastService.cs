using WeatherForecast.Application.Core.Pagination;
using WeatherForecast.Application.Core.Results;
using WeatherForecast.Domain.Entities;
namespace WeatherForecast.Application.Interfaces
{
    public interface IWeatherForecastService
    {
        Result<List<WeatherForecasts>> GetWeatherForecasts(PaginationParams paginationParams);
    }
}
