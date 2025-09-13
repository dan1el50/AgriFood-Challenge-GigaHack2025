using MediatR;
using GigaHack2025.Shared.DTOs;
using GigaHack2025.Core.Enums;

namespace GigaHack2025.UseCases.Commands.Users;

public record RegisterUserCommand(
    string Name,
    string Surname,
    string Email,
    string PhoneNumber,
    string CompanyCode,
    string Password,
    UserRole Role = UserRole.SimpleUser // Default to SimpleUser
) : IRequest<UserResponseDto>;
