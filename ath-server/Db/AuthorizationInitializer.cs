using ath_server.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ath_server.Db;

public enum RoleNames
{
    Admin,
    Manager,
    Staff,
    Client
}

public class AuthorizationInitializer : IAuthorizationInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private SignInManager<IdentityUser> _signInManager;
    private RoleNames[] _roleNames = {RoleNames.Admin, RoleNames.Manager, RoleNames.Staff, RoleNames.Client};
    public AuthorizationInitializer(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }

    public async Task GenerateAdminAndRoles()
    {
        
        foreach (var roleName in _roleNames)
        {
            bool roleExist = _roleManager.RoleExistsAsync(roleName.ToString()).Result;
            if (!roleExist)
                await GenerateRoleWithDefaultAccount(roleName.ToString());
        }
    }
    private async Task GenerateRoleWithDefaultAccount(string roleName)
    {
        IdentityRole role = new IdentityRole();
        role.Name = roleName;
        IdentityResult roleResult = await _roleManager.CreateAsync(role);
        await GenerateDefaultAccount(roleName);
    }

    private async Task GenerateDefaultAccount(string roleName)
    {
        var newAccount = new IdentityUser
        {
            UserName = $"{roleName}@ath.shop.com",
            Email = $"{roleName}@ath.shop.com"
        };
        const string userPWD = "ath2022";
        IdentityResult checkUser = await _userManager.CreateAsync(newAccount, userPWD);
        if (checkUser.Succeeded)
        {
            IdentityResult addRoleResult = await _userManager.AddToRoleAsync(newAccount, roleName);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newAccount);
            var confirmEmailResult = await _userManager.ConfirmEmailAsync(newAccount, code);
        }
    }
}