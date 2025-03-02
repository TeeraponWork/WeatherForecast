using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WeatherForecast.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            // Add test-specific service configuration if needed
            builder.ConfigureServices(services =>
            {
                // You can replace services with test doubles here if needed
                // Example: services.AddScoped<IWeatherService, MockWeatherService>();
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Necessary additional setup when working with .NET 6/7/8 minimal API
            // Ensures proper initialization
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IHostLifetime, NoopHostLifetime>();
            });

            return base.CreateHost(builder);
        }
    }

    // Helper class to prevent test host from shutting down
    public class NoopHostLifetime : IHostLifetime
    {
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        public Task WaitForStartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}