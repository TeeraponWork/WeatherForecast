using WeatherForecast.Application.DTOs.Login;

namespace WeatherForecast.Application.Interfaces.IUseCases.IAuth
{
    public interface IAuthUseCase
    {
        AuthenticationResult Login(LoginRequest request);
        AuthenticationResult RefreshToken(string refreshToken);
    }
}
