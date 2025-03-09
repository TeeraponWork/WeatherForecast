using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeatherForecast.Api.Controllers.BaseController;
using WeatherForecast.Application.DTOs.Login;
using WeatherForecast.Application.DTOs.Token;
using WeatherForecast.Application.Interfaces.IService;

namespace WeatherForecast.Api.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiBaseController
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // จำลองการตรวจสอบ user (ในระบบจริงต้องตรวจจากฐานข้อมูล)
            if (request.Username != "admin" || request.Password != "password")
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var jwtToken = _jwtService.GenerateToken(1, request.Username);
            return Ok(jwtToken);
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(request.Token);
            if (principal == null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }

            var userId = int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
            var userName = principal.FindFirstValue(ClaimTypes.Name);

            // ตรวจสอบว่า Refresh Token ยังถูกต้องหรือไม่
            var storedRefreshToken = _refreshTokenRepository.GetByUserId(userId);
            if (storedRefreshToken == null || storedRefreshToken.Token != request.RefreshToken || storedRefreshToken.IsRevoked || storedRefreshToken.IsExpired)
            {
                return Unauthorized(new { message = "Invalid refresh token" });
            }

            // สร้าง Access Token ใหม่
            var newJwtToken = _jwtService.GenerateToken(userId, userName);

            return Ok(new { token = newJwtToken });
        }

    }
}
