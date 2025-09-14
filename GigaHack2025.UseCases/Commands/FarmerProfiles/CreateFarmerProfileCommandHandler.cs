using FluentValidation;
using GigaHack2025.UseCases.Commands.FarmerProfiles;

namespace GigaHack2025.UseCases.Validators;

public class CreateFarmerProfileCommandValidator : AbstractValidator<CreateFarmerProfileCommand>
{
    public CreateFarmerProfileCommandValidator()
    {
        // Required Fields
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User ID is required.");

        // Business Profile Validations (Optional - only validate if provided)
        RuleFor(x => x.FullName)
            .MaximumLength(100)
            .WithMessage("Full name cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.FullName));

        RuleFor(x => x.CompanyName)
            .MaximumLength(100)
            .WithMessage("Company name cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.CompanyName));

        RuleFor(x => x.CompanyIdno)
            .Length(13)
            .WithMessage("Company IDNO must be exactly 13 characters.")
            .Matches(@"^\d{13}$")
            .WithMessage("Company IDNO must contain only digits.")
            .When(x => !string.IsNullOrEmpty(x.CompanyIdno));

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Please provide a valid email address.")
            .MaximumLength(100)
            .WithMessage("Email cannot exceed 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.Phone)
            .Matches(@"^[\+]?[0-9\-\(\)\s]+$")
            .WithMessage("Please provide a valid phone number format.")
            .MinimumLength(7)
            .WithMessage("Phone number must be at least 7 characters.")
            .MaximumLength(20)
            .WithMessage("Phone number cannot exceed 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.Phone));

        RuleFor(x => x.Address)
            .MaximumLength(200)
            .WithMessage("Address cannot exceed 200 characters.")
            .When(x => !string.IsNullOrEmpty(x.Address));

        // Farm Representative Validations
        RuleFor(x => x.RepFirstName)
            .MaximumLength(50)
            .WithMessage("Representative first name cannot exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.RepFirstName));

        RuleFor(x => x.RepLastName)
            .MaximumLength(50)
            .WithMessage("Representative last name cannot exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.RepLastName));

        RuleFor(x => x.RepDateOfBirth)
            .LessThan(DateTime.Today.AddYears(-18))
            .WithMessage("Representative must be at least 18 years old.")
            .When(x => x.RepDateOfBirth.HasValue);

        RuleFor(x => x.RepIdNumber)
            .MaximumLength(20)
            .WithMessage("Representative ID number cannot exceed 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.RepIdNumber));

        // Agricultural Resources Validations
        RuleFor(x => x.TotalFarmland)
            .GreaterThan(0)
            .WithMessage("Total farmland must be greater than 0.")
            .LessThanOrEqualTo(100000)
            .WithMessage("Total farmland seems unrealistically large (max 100,000 hectares).")
            .When(x => x.TotalFarmland.HasValue);

        // Livestock Validations
        RuleFor(x => x.Cattle)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Cattle count cannot be negative.")
            .LessThanOrEqualTo(50000)
            .WithMessage("Cattle count seems unrealistically large.")
            .When(x => x.Cattle.HasValue);

        RuleFor(x => x.Pigs)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Pigs count cannot be negative.")
            .When(x => x.Pigs.HasValue);

        RuleFor(x => x.Sheep)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Sheep count cannot be negative.")
            .When(x => x.Sheep.HasValue);

        RuleFor(x => x.Goats)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Goats count cannot be negative.")
            .When(x => x.Goats.HasValue);

        RuleFor(x => x.Horses)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Horses count cannot be negative.")
            .When(x => x.Horses.HasValue);

        RuleFor(x => x.Poultry)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Poultry count cannot be negative.")
            .When(x => x.Poultry.HasValue);

        // Human Resources Validations
        RuleFor(x => x.TotalEmployees)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total employees cannot be negative.")
            .When(x => x.TotalEmployees.HasValue);

        RuleFor(x => x.FamilyMembers)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Family members cannot be negative.")
            .When(x => x.FamilyMembers.HasValue);

        RuleFor(x => x.SeasonalWorkers)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Seasonal workers cannot be negative.")
            .When(x => x.SeasonalWorkers.HasValue);

        // Financial Information Validations
        RuleFor(x => x.AnnualIncome)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Annual income cannot be negative.")
            .When(x => x.AnnualIncome.HasValue);

        RuleFor(x => x.AnnualExpenses)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Annual expenses cannot be negative.")
            .When(x => x.AnnualExpenses.HasValue);

        RuleFor(x => x.SubsidiesReceived)
            .MaximumLength(500)
            .WithMessage("Subsidies description cannot exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.SubsidiesReceived));

        RuleFor(x => x.Notes)
            .MaximumLength(2000)
            .WithMessage("Notes cannot exceed 2000 characters.")
            .When(x => !string.IsNullOrEmpty(x.Notes));
    }
}
