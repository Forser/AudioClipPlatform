using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public static class IdentitySeedData
    {
        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, AppIdentityDbContext context)
        {
            string userPassword = "Secret123$";

            bool x = await roleManager.RoleExistsAsync("Admin");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                await roleManager.CreateAsync(role);

                var user = new IdentityUser();
                user.UserName = "Admin";
                user.Email = "admin@admin.com";
                user.EmailConfirmed = true;

                IdentityResult chkUser = await userManager.CreateAsync(user, userPassword);

                var userHandler = new UserHandler();
                userHandler.IdentityId = user.Id;
                context.UserHandler.Update(userHandler);                

                if (chkUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

            x = await roleManager.RoleExistsAsync("Moderator");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "Moderator";
                await roleManager.CreateAsync(role);

                var user = new IdentityUser();
                user.UserName = "Moderator";
                user.Email = "moderator@moderator.com";
                user.EmailConfirmed = true;

                var userHandler = new UserHandler();
                userHandler.IdentityId = user.Id;
                context.UserHandler.Update(userHandler);

                IdentityResult chkUser = await userManager.CreateAsync(user, userPassword);

                if (chkUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Moderator");
                }
            }

            x = await roleManager.RoleExistsAsync("Member");
            if (!x)
            {
                var role = new IdentityRole();
                role.Name = "Member";
                await roleManager.CreateAsync(role);

                var user = new IdentityUser();
                user.UserName = "Forser";
                user.Email = "marckus@gmail.com";
                user.EmailConfirmed = true;

                var userHandler = new UserHandler();
                userHandler.IdentityId = user.Id;
                context.UserHandler.Update(userHandler);

                IdentityResult chkUser = await userManager.CreateAsync(user, userPassword);

                if (chkUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }
        }
    }
}