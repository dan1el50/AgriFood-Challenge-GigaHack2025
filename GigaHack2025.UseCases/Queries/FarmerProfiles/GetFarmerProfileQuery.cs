using MediatR;
using GigaHack2025.Shared.DTOs;

namespace GigaHack2025.UseCases.Queries.FarmerProfiles;

public record GetFarmerProfileQuery(Guid UserId) : IRequest<FarmerProfileDto?>;
