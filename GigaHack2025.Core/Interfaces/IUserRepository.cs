using GigaHack2025.Core.Entities;
using GigaHack2025.Core.Enums;

namespace GigaHack2025.Core.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<User> CreateAsync(User user);
    Task<bool> ExistsAsync(string email);
    Task<List<User>> GetByRoleAsync(UserRole role);
    Task<bool> HasAdminUsersAsync();
}
