using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Untitled.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Untitled.ViewComponents
{
    public class FollowViewComponent : ViewComponent
    {
        private readonly AppIdentityDbContext context;
        private readonly UserManager<IdentityUser> userManager;

        public FollowViewComponent(AppIdentityDbContext ctx, UserManager<IdentityUser> usrManager) { context = ctx; userManager = usrManager; }

        public async Task<IViewComponentResult> InvokeAsync(string profileName)
        {
            var userIdentity = context.Users.Where(a => a.UserName == profileName).FirstOrDefault();
            var loggedInUser = await userManager.GetUserAsync(HttpContext.User);

            string result = "Not able to follow!";

            if(loggedInUser != null)
            {
                if(userIdentity.UserName != loggedInUser.UserName)
                {
                    result = "You can follow!";
                }
                else
                {
                    result = "You are logged in but viewing your own profile!";
                }

            }

            return View("Default", result);
        }
    }
}