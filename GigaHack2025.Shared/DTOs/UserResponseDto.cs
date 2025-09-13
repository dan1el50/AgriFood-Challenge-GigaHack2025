using GigaHack2025.Core.Enums;

namespace GigaHack2025.Shared.DTOs;

public class UserResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsAdmin => Role == UserRole.Admin;
    public bool IsSimpleUser => Role == UserRole.SimpleUser;
}
