using GigaHack2025.Core.Enums;
using GigaHack2025.Infrastructure.Data;
using GigaHack2025.Shared.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;

namespace GigaHack2025.Tests;

public class AuthControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public AuthControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Replace SQL Server with in-memory database for testing
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("TestDatabase"));
            });
        });

        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Register_SimpleUser_ReturnsSuccess()
    {
        // Arrange
        var request = new RegisterUserDto
        {
            Name = "John",
            Surname = "Doe",
            Email = "john.doe@test.com",
            PhoneNumber = "+1234567890",
            CompanyCode = "1234567890123",
            Password = "SecurePass123!",
            Role = UserRole.SimpleUser
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserResponseDto>();
        Assert.NotNull(result);
        Assert.Equal("John", result.Name);
        Assert.Equal(UserRole.SimpleUser, result.Role);
        Assert.False(result.IsAdmin);
    }

    [Fact]
    public async Task Register_AdminUser_ReturnsSuccess()
    {
        // Arrange
        var request = new RegisterUserDto
        {
            Name = "Jane",
            Surname = "Admin",
            Email = "jane.admin@test.com",
            PhoneNumber = "+0987654321",
            CompanyCode = "9876543210987",
            Password = "AdminPass123!"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Auth/register/admin", request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<UserResponseDto>();
        Assert.NotNull(result);
        Assert.Equal("Jane", result.Name);
        Assert.Equal(UserRole.Admin, result.Role);
        Assert.True(result.IsAdmin);
    }

    [Fact]
    public async Task Register_DuplicateEmail_ReturnsBadRequest()
    {
        // Arrange
        var request = new RegisterUserDto
        {
            Name = "John",
            Surname = "Doe",
            Email = "duplicate@test.com",
            PhoneNumber = "+1234567890",
            CompanyCode = "1234567890123",
            Password = "SecurePass123!",
            Role = UserRole.SimpleUser
        };

        // Act - Register first user
        await _client.PostAsJsonAsync("/api/Auth/register", request);

        // Act - Try to register with same email
        var response = await _client.PostAsJsonAsync("/api/Auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Register_InvalidEmail_ReturnsBadRequest()
    {
        // Arrange
        var request = new RegisterUserDto
        {
            Name = "John",
            Surname = "Doe",
            Email = "invalid-email",
            PhoneNumber = "+1234567890",
            CompanyCode = "1234567890123",
            Password = "SecurePass123!",
            Role = UserRole.SimpleUser
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/Auth/register", request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
}
