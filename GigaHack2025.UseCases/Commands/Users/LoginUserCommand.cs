using MediatR;
using GigaHack2025.Shared.DTOs;

namespace GigaHack2025.UseCases.Commands.Users;

public record LoginUserCommand(
    string Email,
    string Password
) : IRequest<UserResponseDto>;
