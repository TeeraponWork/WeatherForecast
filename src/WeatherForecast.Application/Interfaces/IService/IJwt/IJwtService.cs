using System.Security.Claims;
using WeatherForecast.Domain.Models.Token;

namespace WeatherForecast.Application.Interfaces.IService
{
    public interface IJwtService
    {
        JwtToken GenerateToken(int userId, string userName);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
