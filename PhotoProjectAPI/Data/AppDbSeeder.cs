﻿using PhotoProjectAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PhotoProjectAPI.Data
{
    public class AppDbSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                context.SaveChanges();
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

             
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var adminUser = await userManager.FindByNameAsync("admin");
                if (adminUser == null)
                {
                    var newAdminUser = new User()
                    {
                        UserName = "admin",
                    };
                    await userManager.CreateAsync(newAdminUser, "Haslo56!");
                    await userManager.AddClaimAsync(newAdminUser, new Claim(ClaimTypes.Role, "ADMIN"));
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }              
                var user = await userManager.FindByNameAsync("user");
                if (user == null)
                {
                    var newUser = new User()
                    {
                        UserName = "user"
                    };
                    await userManager.CreateAsync(newUser, "Haslo56!");
                    await userManager.AddToRoleAsync(newUser, UserRoles.User);
                }
            }
        }
    }
}

