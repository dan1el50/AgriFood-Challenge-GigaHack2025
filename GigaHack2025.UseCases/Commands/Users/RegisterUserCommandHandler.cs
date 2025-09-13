using MediatR;
using GigaHack2025.Core.Entities;
using GigaHack2025.Core.Interfaces;
using GigaHack2025.Shared.DTOs;
using GigaHack2025.UseCases.Commands.Users;
using BCrypt.Net;

namespace GigaHack2025.UseCases.Commands.Users;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserResponseDto>
{
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (await _userRepository.ExistsAsync(request.Email))
        {
            throw new InvalidOperationException($"User with email {request.Email} already exists.");
        }

        // If trying to create first admin and no admin exists, allow it
        // Otherwise, admin creation should be restricted (you can add authorization logic here)
        var hasAdminUsers = await _userRepository.HasAdminUsersAsync();

        // Hash the password
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        // Create new user
        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email.ToLowerInvariant(),
            PhoneNumber = request.PhoneNumber,
            CompanyCode = request.CompanyCode,
            PasswordHash = passwordHash,
            Role = request.Role
        };

        // Save user to database
        var createdUser = await _userRepository.CreateAsync(user);

        // Return response DTO
        return new UserResponseDto
        {
            Id = createdUser.Id,
            Name = createdUser.Name,
            Surname = createdUser.Surname,
            Email = createdUser.Email,
            PhoneNumber = createdUser.PhoneNumber,
            CompanyCode = createdUser.CompanyCode,
            Role = createdUser.Role,
            CreatedAt = createdUser.CreatedAt
        };
    }
}
