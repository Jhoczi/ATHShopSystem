using ath_server.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ath_server.Db;

public class AuthorizationInitializer : IAuthorizationInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private SignInManager<IdentityUser> _signInManager;

    public AuthorizationInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;

    }

    public void GenerateAdminAndRoles()
    {
        //var admin = _userManager.FindByEmailAsync("damiangrygiercz@gmail.com").Result;
        // if (admin == null)
        // {
        //     _userManager.CreateAsync(new IdentityUser()
        //     {
        //         UserName = "damiangrygiercz@gmail.com",
        //         Email = "docker2021"
        //     }, "docker2021");
        //     admin = _userManager.FindByEmailAsync("damiangrygiercz@gmail.com").Result;
        //     var code = _userManager.GenerateEmailConfirmationTokenAsync(admin).Result;
        //     var result = _userManager.ConfirmEmailAsync(admin, code).Result;
        // }
        //
        // if (_roleManager.Roles.Count() == 0)
        // {
        //     _roleManager.CreateAsync(new IdentityRole("Admin"));
        //     _roleManager.CreateAsync(new IdentityRole("Moderator"));
        //     _roleManager.CreateAsync(new IdentityRole("Cliemt"));
        //     _roleManager.CreateAsync(new IdentityRole("Staff"));
        //
        //     _userManager.AddToRoleAsync(admin, "Admin");
        // }

        string[] roleNames = { "Admin", "User" };
        bool adminRoleExist = _roleManager.RoleExistsAsync(roleNames[0]).Result;
        
        
        if (!adminRoleExist)
        {
            IdentityRole role = new IdentityRole();
            role.Name = "Admin";
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;

            var admin = new IdentityUser
            {
                UserName = "admin@ath.shop.com",
                Email = "admin@ath.shop.com"
            };
            const string userPWD = "ath2022";
            IdentityResult checkUser = _userManager.CreateAsync(admin, userPWD).Result;
            if (checkUser.Succeeded)
            {
                IdentityResult addRoleResult = _userManager.AddToRoleAsync(admin, "Admin").Result;
                var code = _userManager.GenerateEmailConfirmationTokenAsync(admin).Result;
                var confirmEmailResult = _userManager.ConfirmEmailAsync(admin, code).Result;
            }
            
            // Create other roles:
           

            foreach (var roleName in roleNames)
            {
                var roleExist = _roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = _roleManager.CreateAsync(new IdentityRole(roleName)).Result;
                }
            }
        }
        
    }
}