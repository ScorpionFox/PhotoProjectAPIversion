using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;
using PhotoProjectAPI.Dataset;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PhotoProjectAPI.Data
{
    public class AppDbSeeder
    {
        //Tutaj wypelni baze uzytkownikami - najlepiej 1 administrator i 1 defaultowy, ✓
        //stworzyć metode co sie wywola w program.cs ✓

        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppDbSeeder(
            AppDbContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void SeedDatabase()
        {
            _context.Database.EnsureCreated();
        }

        public async Task SeedRolesAsync()
        {
            await SeedRole(LoginRoles.Admin);
            await SeedRole(LoginRoles.User);
        }

        public async Task SeedUsersAsync()
        {
            await SeedUser("admin", LoginRoles.Admin);
            await SeedUser("user", LoginRoles.User);
        }

        private async Task SeedRole(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        private async Task SeedUser(string username, string roleName)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                var newUser = new User
                {
                    UserName = username
                };

                await _userManager.CreateAsync(newUser, "password");

                await _userManager.AddClaimAsync(newUser, new Claim(ClaimTypes.Role, roleName));
                await _userManager.AddToRoleAsync(newUser, roleName);
            }
        }
    }

}