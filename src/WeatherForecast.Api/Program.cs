using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using WeatherForecast.Api.Middleware;
using WeatherForecast.Api.Swagger;
using WeatherForecast.Application.Extensions;
using WeatherForecast.Application.Interfaces.IService;
using WeatherForecast.Application.Services.Jwt;
using WeatherForecast.Domain.Models.Token;

var builder = WebApplication.CreateBuilder(args);

// Add JWT settings from appsettings.json
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

// Register JwtService
builder.Services.AddScoped<IJwtService, JwtService>();

// Configure Authentication
var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }