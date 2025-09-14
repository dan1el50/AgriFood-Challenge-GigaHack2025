using GigaHack2025.Core.Enums;

namespace GigaHack2025.Core.Entities;

public class FarmerProfile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // Required - links to User

    // Business Profile - All Optional
    public string? FullName { get; set; }
    public string? CompanyName { get; set; }
    public LegalFormType? LegalForm { get; set; }
    public string? CompanyIdno { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }

    // Farm Representative - All Optional
    public string? RepFirstName { get; set; }
    public string? RepLastName { get; set; }
    public DateTime? RepDateOfBirth { get; set; }
    public GenderType? RepGender { get; set; }
    public string? RepIdNumber { get; set; }

    // Activities & Resources - All Optional
    public bool? CropCultivation { get; set; }
    public bool? AnimalHusbandry { get; set; }
    public bool? MixedFarming { get; set; }
    public bool? ProcessingActivities { get; set; }
    public decimal? TotalFarmland { get; set; }

    // Land Use Types - All Optional
    public bool? Arable { get; set; }
    public bool? Orchards { get; set; }
    public bool? Vineyards { get; set; }
    public bool? Pastures { get; set; }
    public bool? Hayfields { get; set; }
    public bool? Greenhouses { get; set; }

    // Livestock Register - All Optional
    public int? Cattle { get; set; }
    public int? Pigs { get; set; }
    public int? Sheep { get; set; }
    public int? Goats { get; set; }
    public int? Horses { get; set; }
    public int? Poultry { get; set; }

    // Human Resources - All Optional
    public int? TotalEmployees { get; set; }
    public int? FamilyMembers { get; set; }
    public int? SeasonalWorkers { get; set; }

    // Financial Information - All Optional
    public decimal? AnnualIncome { get; set; }
    public decimal? AnnualExpenses { get; set; }
    public string? SubsidiesReceived { get; set; }

    // Notes - Optional
    public string? Notes { get; set; }

    // Consent - Optional (can be updated later)
    public bool? ConsentDataSharing { get; set; }
    public bool? AcceptPrivacyPolicy { get; set; }

    // Audit Fields - Required
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation Properties
    public User User { get; set; } = null!;

    public FarmerProfile()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
