using GigaHack2025.Core.Enums;

namespace GigaHack2025.Shared.DTOs;

public class FarmerProfileDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    // Business Profile
    public string FullName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public LegalFormType LegalForm { get; set; }
    public string CompanyIdno { get; set; } = string.Empty;
    public DateTime? RegistrationDate { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    // Farm Representative
    public string RepFirstName { get; set; } = string.Empty;
    public string RepLastName { get; set; } = string.Empty;
    public DateTime? RepDateOfBirth { get; set; }
    public GenderType RepGender { get; set; }
    public string RepIdNumber { get; set; } = string.Empty;

    // Activities & Resources
    public bool CropCultivation { get; set; }
    public bool AnimalHusbandry { get; set; }
    public bool MixedFarming { get; set; }
    public bool ProcessingActivities { get; set; }
    public decimal? TotalFarmland { get; set; }

    // Land Use Types
    public bool Arable { get; set; }
    public bool Orchards { get; set; }
    public bool Vineyards { get; set; }
    public bool Pastures { get; set; }
    public bool Hayfields { get; set; }
    public bool Greenhouses { get; set; }

    // Livestock Register
    public int? Cattle { get; set; }
    public int? Pigs { get; set; }
    public int? Sheep { get; set; }
    public int? Goats { get; set; }
    public int? Horses { get; set; }
    public int? Poultry { get; set; }

    // Human Resources
    public int? TotalEmployees { get; set; }
    public int? FamilyMembers { get; set; }
    public int? SeasonalWorkers { get; set; }

    // Financial Information
    public decimal? AnnualIncome { get; set; }
    public decimal? AnnualExpenses { get; set; }
    public string SubsidiesReceived { get; set; } = string.Empty;

    // Notes
    public string Notes { get; set; } = string.Empty;

    // Consent
    public bool ConsentDataSharing { get; set; }
    public bool AcceptPrivacyPolicy { get; set; }

    // Metadata
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
