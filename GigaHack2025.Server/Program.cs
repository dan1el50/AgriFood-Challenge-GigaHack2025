using FluentValidation;
using GigaHack2025.Core.Interfaces;
using GigaHack2025.Infrastructure.Data;
using GigaHack2025.Infrastructure.Repositories;
using GigaHack2025.UseCases.Commands.FarmerProfiles;
using GigaHack2025.UseCases.Commands.Users;
using GigaHack2025.UseCases.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "GigaHack2025 API",
        Version = "v1",
        Description = "AgroHub API for user registration and authentication"
    });
});

// Add Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));

// Add FluentValidation - This fixes the validator registration error
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();

// Add repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFarmerProfileRepository, FarmerProfileRepository>();

// Add CORS - Updated for your actual ports and Swagger UI
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins(
            "https://localhost:64755", // Your Blazor client HTTPS
            "http://localhost:64757",  // Your Blazor client HTTP
            "https://localhost:64756", // Allow Swagger UI (same as server)
            "http://localhost:64758",  // Allow Swagger UI HTTP
            "http://localhost:5240",   // Keep old ones for compatibility
            "https://localhost:7001",
            "http://localhost:5000",
            "https://localhost:5001"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GigaHack2025 API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");
app.UseAuthorization();
app.MapControllers();

// Weather forecast endpoint for testing
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast(
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
