using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectUntitled.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectUntitled.ViewComponents
{
    [Authorize]
    public class UserClipsViewComponent : ViewComponent
    {
        private UserManager<IdentityUser> userManager;
        private readonly IClipsRepository repository;

        public UserClipsViewComponent(UserManager<IdentityUser> usrManager, IClipsRepository repo)
        {
            userManager = usrManager;
            repository = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loggedInUser = await userManager.GetUserAsync(HttpContext.User);
            var userClips = new List<Clips>();

            if (loggedInUser != null)
            {
                userClips = repository.GetClipsForUser(loggedInUser);
            }

            return View(userClips);
        }
    }
}
