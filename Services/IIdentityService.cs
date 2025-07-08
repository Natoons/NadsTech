using NadsTech.Models;

namespace NadsTech.Services;

public interface IIdentityService
{
    Task InitializeRolesAsync();
    Task CreateDefaultAdminAsync();
    Task<bool> IsUserInRoleAsync(string userId, string roleName);
    Task<IList<string>> GetUserRolesAsync(string userId);
} 