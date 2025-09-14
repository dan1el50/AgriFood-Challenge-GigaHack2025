using MediatR;
using GigaHack2025.Core.Interfaces;
using GigaHack2025.Core.Enums;
using GigaHack2025.Shared.DTOs;
using GigaHack2025.UseCases.Queries.FarmerProfiles;

namespace GigaHack2025.UseCases.Queries.FarmerProfiles;

public class GetFarmerProfileQueryHandler : IRequestHandler<GetFarmerProfileQuery, FarmerProfileDto?>
{
    private readonly IFarmerProfileRepository _farmerProfileRepository;

    public GetFarmerProfileQueryHandler(IFarmerProfileRepository farmerProfileRepository)
    {
        _farmerProfileRepository = farmerProfileRepository;
    }

    public async Task<FarmerProfileDto?> Handle(GetFarmerProfileQuery request, CancellationToken cancellationToken)
    {
        var profile = await _farmerProfileRepository.GetByUserIdAsync(request.UserId);

        if (profile == null)
            return null;

        return new FarmerProfileDto
        {
            Id = profile.Id,
            UserId = profile.UserId,

            // Business Profile - Use null-coalescing operator for nullable fields
            FullName = profile.FullName ?? string.Empty,
            CompanyName = profile.CompanyName ?? string.Empty,
            LegalForm = profile.LegalForm ?? LegalFormType.Individual, // Default to Individual
            CompanyIdno = profile.CompanyIdno ?? string.Empty,
            RegistrationDate = profile.RegistrationDate,
            Email = profile.Email ?? string.Empty,
            Phone = profile.Phone ?? string.Empty,
            Address = profile.Address ?? string.Empty,

            // Farm Representative
            RepFirstName = profile.RepFirstName ?? string.Empty,
            RepLastName = profile.RepLastName ?? string.Empty,
            RepDateOfBirth = profile.RepDateOfBirth,
            RepGender = profile.RepGender ?? GenderType.NotSpecified, // Default to NotSpecified
            RepIdNumber = profile.RepIdNumber ?? string.Empty,

            // Activities & Resources - Default nullable bools to false
            CropCultivation = profile.CropCultivation ?? false,
            AnimalHusbandry = profile.AnimalHusbandry ?? false,
            MixedFarming = profile.MixedFarming ?? false,
            ProcessingActivities = profile.ProcessingActivities ?? false,
            TotalFarmland = profile.TotalFarmland,

            // Land Use Types - Default to false
            Arable = profile.Arable ?? false,
            Orchards = profile.Orchards ?? false,
            Vineyards = profile.Vineyards ?? false,
            Pastures = profile.Pastures ?? false,
            Hayfields = profile.Hayfields ?? false,
            Greenhouses = profile.Greenhouses ?? false,

            // Livestock Register - Keep as nullable in DTO
            Cattle = profile.Cattle,
            Pigs = profile.Pigs,
            Sheep = profile.Sheep,
            Goats = profile.Goats,
            Horses = profile.Horses,
            Poultry = profile.Poultry,

            // Human Resources - Keep as nullable in DTO
            TotalEmployees = profile.TotalEmployees,
            FamilyMembers = profile.FamilyMembers,
            SeasonalWorkers = profile.SeasonalWorkers,

            // Financial Information - Keep as nullable in DTO
            AnnualIncome = profile.AnnualIncome,
            AnnualExpenses = profile.AnnualExpenses,
            SubsidiesReceived = profile.SubsidiesReceived ?? string.Empty,

            // Notes
            Notes = profile.Notes ?? string.Empty,

            // Consent - Default to false for safety
            ConsentDataSharing = profile.ConsentDataSharing ?? false,
            AcceptPrivacyPolicy = profile.AcceptPrivacyPolicy ?? false,

            // Audit Fields
            CreatedAt = profile.CreatedAt,
            UpdatedAt = profile.UpdatedAt
        };
    }
}
