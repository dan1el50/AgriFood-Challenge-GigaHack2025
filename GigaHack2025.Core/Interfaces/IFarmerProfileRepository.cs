using GigaHack2025.Core.Entities;

namespace GigaHack2025.Core.Interfaces;

public interface IFarmerProfileRepository
{
    Task<FarmerProfile?> GetByIdAsync(Guid id);
    Task<FarmerProfile?> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<FarmerProfile>> GetAllAsync();
    Task<FarmerProfile> CreateAsync(FarmerProfile profile);
    Task<FarmerProfile> UpdateAsync(FarmerProfile profile);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> ExistsAsync(Guid userId);
    Task<IEnumerable<FarmerProfile>> SearchAsync(string searchTerm);
}
