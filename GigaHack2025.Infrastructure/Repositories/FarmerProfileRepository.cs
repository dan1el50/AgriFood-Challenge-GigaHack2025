using Microsoft.EntityFrameworkCore;
using GigaHack2025.Core.Entities;
using GigaHack2025.Core.Interfaces;
using GigaHack2025.Infrastructure.Data;

namespace GigaHack2025.Infrastructure.Repositories;

public class FarmerProfileRepository : IFarmerProfileRepository
{
    private readonly ApplicationDbContext _context;

    public FarmerProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FarmerProfile?> GetByIdAsync(Guid id)
    {
        return await _context.FarmerProfiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<FarmerProfile?> GetByUserIdAsync(Guid userId)
    {
        return await _context.FarmerProfiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<IEnumerable<FarmerProfile>> GetAllAsync()
    {
        return await _context.FarmerProfiles
            .Include(p => p.User)
            .Where(p => p.IsActive)
            .ToListAsync();
    }

    public async Task<FarmerProfile> CreateAsync(FarmerProfile profile)
    {
        _context.FarmerProfiles.Add(profile);
        await _context.SaveChangesAsync();
        return profile;
    }

    public async Task<FarmerProfile> UpdateAsync(FarmerProfile profile)
    {
        profile.UpdatedAt = DateTime.UtcNow;
        _context.FarmerProfiles.Update(profile);
        await _context.SaveChangesAsync();
        return profile;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var profile = await GetByIdAsync(id);
        if (profile == null)
            return false;

        profile.IsActive = false;
        profile.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(Guid userId)
    {
        return await _context.FarmerProfiles
            .AnyAsync(p => p.UserId == userId && p.IsActive);
    }

    public async Task<IEnumerable<FarmerProfile>> SearchAsync(string searchTerm)
    {
        return await _context.FarmerProfiles
            .Include(p => p.User)
            .Where(p => p.IsActive &&
                   (p.FullName.Contains(searchTerm) ||
                    p.CompanyName.Contains(searchTerm) ||
                    p.Email.Contains(searchTerm)))
            .ToListAsync();
    }
}
