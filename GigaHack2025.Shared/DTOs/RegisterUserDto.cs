using GigaHack2025.Core.Enums;

namespace GigaHack2025.Shared.DTOs;

public class RegisterUserDto
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.SimpleUser; // Default to SimpleUser
}
