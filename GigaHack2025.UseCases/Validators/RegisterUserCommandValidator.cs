using FluentValidation;
using GigaHack2025.UseCases.Commands.Users;
using GigaHack2025.Core.Enums;

namespace GigaHack2025.UseCases.Validators;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name can only contain letters and spaces");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required")
            .MaximumLength(50).WithMessage("Surname cannot exceed 50 characters")
            .Matches(@"^[a-zA-Z\s]+$").WithMessage("Surname can only contain letters and spaces");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format")
            .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .Matches(@"^[\+]?[0-9\-\(\)\s]+$").WithMessage("Invalid phone number format");

        RuleFor(x => x.CompanyCode)
            .NotEmpty().WithMessage("Company code (IDNO) is required")
            .Length(13).WithMessage("Company code must be exactly 13 characters")
            .Matches(@"^\d{13}$").WithMessage("Company code must contain only digits");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter")
            .Matches(@"\d").WithMessage("Password must contain at least one number")
            .Matches(@"[\!\?\*\.\@\#\$\%\^\&]").WithMessage("Password must contain at least one special character");

        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Invalid role specified")
            .Must(role => role == UserRole.SimpleUser || role == UserRole.Admin)
            .WithMessage("Role must be either SimpleUser or Admin");
    }
}
