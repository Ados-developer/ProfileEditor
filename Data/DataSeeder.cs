using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProfileEditor.Models;

namespace ProfileEditor.Data
{
    public static class DataSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdminUser(UserManager<UserModel> userManager, ProfileEditorDbContext context)
        {
            string adminEmail = "admin@admin.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var user = new UserModel
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    // Vytvorenie profilu pre admina
                    var profile = new ProfileModel
                    {
                        UserId = user.Id,
                        FirstName = "",
                        LastName = "",
                        Adress = "",
                        City = "",
                        Country = "",
                        ContactNumber = "",
                        Age = 0,
                        Description = "",
                        PhotoImage = "default.png"
                    };
                    context.Profiles.Add(profile);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
