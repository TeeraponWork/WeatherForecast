namespace WeatherForecast.Domain.Models.Token
{
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public int TokenExpirationMinutes { get; set; }
        public int RefreshTokenExpirationDays { get; set; }
    }
}
