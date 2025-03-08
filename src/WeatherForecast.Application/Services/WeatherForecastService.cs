using WeatherForecast.Application.Core.Exceptions;
using WeatherForecast.Application.Core.Pagination;
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

        public Result<List<WeatherForecasts>> GetWeatherForecasts(PaginationParams paginationParams)
        {
            try
            {
                var result = Enumerable.Range(1, 100000).Select(index => new WeatherForecasts
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).AsQueryable();

                var pagedResult = result.ToPagedList(paginationParams.PageNumber, paginationParams.PageSize);
                return Result<List<WeatherForecasts>>.Success(pagedResult.Items, pagedResult.Pagination);
            }
            catch (Exception ex)
            {
                return Result<List<WeatherForecasts>>.Failure(ex.Message);
            }
        }
    }
}
