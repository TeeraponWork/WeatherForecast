using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Api.Controllers.BaseController;
using WeatherForecast.Application.Core.Pagination;
using WeatherForecast.Application.Core.Results;
using WeatherForecast.Application.Interfaces;

namespace WeatherForecast.Api.Controllers;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class WeatherForecastController : ApiBaseController
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IActionResult Get([FromQuery] PaginationParams paginationParams)
    {
        var forecasts = _weatherForecastService.GetWeatherForecasts(paginationParams);
        return Ok(forecasts);
    }

    //[HttpPost(Name = "GetWeatherForecast")]
    //[ApiExplorerSettings(IgnoreApi = false)] 
    //public IActionResult Post()
    //{
    //    var forecasts = _weatherForecastService.GetWeatherForecasts();
    //    return HandleResponse(forecasts);
    //}
}
