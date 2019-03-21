using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public static class IdentitySeedData
    {
        public static async Task EnsurePopulated(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
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

                IdentityResult chkUser = await userManager.CreateAsync(user, userPassword);

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

                IdentityResult chkUser = await userManager.CreateAsync(user, userPassword);

                if (chkUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Member");
                }
            }
        }
    }
}