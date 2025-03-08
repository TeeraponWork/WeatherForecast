using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Core.Results;

namespace WeatherForecast.Api.Controllers.BaseController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected IActionResult HandleResponse<T>(T value)
        {
            if (value == null)
            {
                return NotFound();
            }

            return Ok(value); //เปลี่ยนใช้ Result<T> . Success
        }
        protected IActionResult HandleErrorResponse(string errorMessage)
        {
            return BadRequest(new { message = errorMessage });
        }
    }
}
