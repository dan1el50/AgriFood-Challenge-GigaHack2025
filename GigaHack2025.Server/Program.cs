using FluentValidation;
using GigaHack2025.Core.Interfaces;
using GigaHack2025.Infrastructure.Data;
using GigaHack2025.Infrastructure.Repositories;
using GigaHack2025.UseCases.Commands.FarmerProfiles;
using GigaHack2025.UseCases.Commands.Users;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Replace OpenAPI with Swagger
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

// New registration (scans UseCases assembly where handlers are)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(
    typeof(CreateFarmerProfileCommand).Assembly
));


// Add Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Replace your current CORS configuration with this:
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5240",    // Your actual client port
            "https://localhost:7001",   // HTTPS version if needed
            "http://localhost:5000",    // Keep the old ones for compatibility
            "https://localhost:5001"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add this to your existing services
builder.Services.AddScoped<IFarmerProfileRepository, FarmerProfileRepository>();

// Validators are already registered via assembly scanning


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable Swagger in development
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GigaHack2025 API v1");
        c.RoutePrefix = "swagger"; // This makes it accessible at /swagger
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorClient");
app.UseAuthorization();
app.MapControllers();

// Keep your weather forecast endpoint for testing
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
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
