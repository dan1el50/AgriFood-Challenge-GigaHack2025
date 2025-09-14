using MediatR;
using GigaHack2025.Shared.DTOs;
using GigaHack2025.Core.Enums;

namespace GigaHack2025.UseCases.Commands.FarmerProfiles;

public record CreateFarmerProfileCommand(
    Guid UserId,
    string? FullName = null,
    string? CompanyName = null,
    LegalFormType? LegalForm = null,
    string? CompanyIdno = null,
    DateTime? RegistrationDate = null,
    string? Email = null,
    string? Phone = null,
    string? Address = null,
    string? RepFirstName = null,
    string? RepLastName = null,
    DateTime? RepDateOfBirth = null,
    GenderType? RepGender = null,
    string? RepIdNumber = null,
    bool? CropCultivation = null,
    bool? AnimalHusbandry = null,
    bool? MixedFarming = null,
    bool? ProcessingActivities = null,
    decimal? TotalFarmland = null,
    bool? Arable = null,
    bool? Orchards = null,
    bool? Vineyards = null,
    bool? Pastures = null,
    bool? Hayfields = null,
    bool? Greenhouses = null,
    int? Cattle = null,
    int? Pigs = null,
    int? Sheep = null,
    int? Goats = null,
    int? Horses = null,
    int? Poultry = null,
    int? TotalEmployees = null,
    int? FamilyMembers = null,
    int? SeasonalWorkers = null,
    decimal? AnnualIncome = null,
    decimal? AnnualExpenses = null,
    string? SubsidiesReceived = null,
    string? Notes = null,
    bool? ConsentDataSharing = null,
    bool? AcceptPrivacyPolicy = null
) : IRequest<FarmerProfileDto>;

