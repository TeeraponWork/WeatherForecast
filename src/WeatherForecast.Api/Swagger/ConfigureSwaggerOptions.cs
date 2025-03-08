using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WeatherForecast.Api.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "WeatherForecast API",
                Version = description.ApiVersion.ToString(),
                Description = "The WeatherForecast API provides weather forecasting services, allowing users to retrieve weather data for different regions.", // รายละเอียดของ API
                Contact = new OpenApiContact
                {
                    Name = "WeatherForecast Team",
                    Email = "support@weatherforecast.com",
                    Url = new Uri("https://www.weatherforecast.com/contact") 
                },
                License = new OpenApiLicense
                {
                    Name = "Proprietary License",  
                    Url = new Uri("https://www.weatherforecast.com/license") 
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

    }
}
