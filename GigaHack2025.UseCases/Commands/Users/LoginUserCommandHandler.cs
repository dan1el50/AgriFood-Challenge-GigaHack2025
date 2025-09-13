using MediatR;
using GigaHack2025.Core.Interfaces;
using GigaHack2025.Shared.DTOs;
using GigaHack2025.UseCases.Commands.Users;
using BCrypt.Net;

namespace GigaHack2025.UseCases.Commands.Users;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;

    public LoginUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        // Find user by email
        var user = await _userRepository.GetByEmailAsync(request.Email.ToLowerInvariant());

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Verify password
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid email or password.");
        }

        // Check if user is active
        if (!user.IsActive)
        {
            throw new UnauthorizedAccessException("Account is deactivated.");
        }

        // Return user data (without sensitive information)
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            CompanyCode = user.CompanyCode,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}
