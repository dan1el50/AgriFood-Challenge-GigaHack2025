using GigaHack2025.Core.Enums;

namespace GigaHack2025.Shared.DTOs;

public class CreateFarmerProfileDto
{
    public Guid UserId { get; set; } // Required

    // All other fields are optional
    public string? FullName { get; set; }
    public string? CompanyName { get; set; }
    public LegalFormType? LegalForm { get; set; }
    public string? CompanyIdno { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }

    public string? RepFirstName { get; set; }
    public string? RepLastName { get; set; }
    public DateTime? RepDateOfBirth { get; set; }
    public GenderType? RepGender { get; set; }
    public string? RepIdNumber { get; set; }

    public bool? CropCultivation { get; set; }
    public bool? AnimalHusbandry { get; set; }
    public bool? MixedFarming { get; set; }
    public bool? ProcessingActivities { get; set; }
    public decimal? TotalFarmland { get; set; }

    public bool? Arable { get; set; }
    public bool? Orchards { get; set; }
    public bool? Vineyards { get; set; }
    public bool? Pastures { get; set; }
    public bool? Hayfields { get; set; }
    public bool? Greenhouses { get; set; }

    public int? Cattle { get; set; }
    public int? Pigs { get; set; }
    public int? Sheep { get; set; }
    public int? Goats { get; set; }
    public int? Horses { get; set; }
    public int? Poultry { get; set; }

    public int? TotalEmployees { get; set; }
    public int? FamilyMembers { get; set; }
    public int? SeasonalWorkers { get; set; }

    public decimal? AnnualIncome { get; set; }
    public decimal? AnnualExpenses { get; set; }
    public string? SubsidiesReceived { get; set; }

    public string? Notes { get; set; }

    public bool? ConsentDataSharing { get; set; }
    public bool? AcceptPrivacyPolicy { get; set; }
}
