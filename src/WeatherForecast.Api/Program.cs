using Asp.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using WeatherForecast.Api.Middleware;
using WeatherForecast.Api.Swagger;
using WeatherForecast.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Gzip compression middleware
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;  // สามารถเปิดใช้การบีบอัดสำหรับ HTTPS
    options.Providers.Add<GzipCompressionProvider>();  // ใช้ Gzip
    options.Providers.Add<BrotliCompressionProvider>();  // Brotli (optional)
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//เพิ่มเวอร์ชัน SwaggerGen
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<SwaggerDefaultValues>();
});

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddApiVersioning().AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});
//จบเวอร์ชัน SwaggerGen

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Use compression middleware
app.UseResponseCompression();

// Add the middleware to the pipeline
app.UseMiddleware<CustomMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    //เพิ่มเวอร์ชันใน swagger
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }