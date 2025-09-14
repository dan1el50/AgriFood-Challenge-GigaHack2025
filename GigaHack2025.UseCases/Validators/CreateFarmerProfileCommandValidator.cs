using FluentValidation;
using GigaHack2025.UseCases.Commands.FarmerProfiles;

namespace GigaHack2025.UseCases.Validators;

public class CreateFarmerProfileCommandValidator : AbstractValidator<CreateFarmerProfileCommand>
{
    public CreateFarmerProfileCommandValidator()
    {
        // Only UserId is required - all other fields are optional
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        // Optional field validations - only validate if provided
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Please provide a valid email address.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .Matches(@"^[\+]?[0-9\-\(\)\s]+$")
            .WithMessage("Please provide a valid phone number format.")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.CompanyIdno)
            .Length(13)
            .WithMessage("Company IDNO must be exactly 13 characters.")
            .When(x => !string.IsNullOrEmpty(x.CompanyIdno));

        RuleFor(x => x.TotalFarmland)
            .GreaterThan(0)
            .WithMessage("Total farmland must be greater than 0.")
            .When(x => x.TotalFarmland.HasValue);

        RuleFor(x => x.AnnualIncome)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Annual income cannot be negative.")
            .When(x => x.AnnualIncome.HasValue);

        RuleFor(x => x.AnnualExpenses)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Annual expenses cannot be negative.")
            .When(x => x.AnnualExpenses.HasValue);
    }
}
