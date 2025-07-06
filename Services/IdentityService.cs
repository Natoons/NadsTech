using Microsoft.AspNetCore.Identity;
using NadsTech.Models;

namespace NadsTech.Services;

public class IdentityService : IIdentityService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeRolesAsync()
    {
        var roles = new[] { "Admin", "Author", "Reader" };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    public async Task CreateDefaultAdminAsync()
    {
        var adminEmail = "admin@nadstech.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "Admin",
                LastName = "NadsTech",
                EmailConfirmed = true,
                CreatedAt = DateTime.Now
            };

            var result = await _userManager.CreateAsync(adminUser, "Admin123!");
            
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }

    public async Task<bool> IsUserInRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return false;
        
        return await _userManager.IsInRoleAsync(user, roleName);
    }

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return new List<string>();
        
        return await _userManager.GetRolesAsync(user);
    }
} 