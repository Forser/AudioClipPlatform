using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectUntitled.Models;
using System.Threading.Tasks;

namespace ProjectUntitled.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileRepository profileRepository;

        public ProfileController(IProfileRepository profileRepo)
        {
            profileRepository = profileRepo;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> GetProfile(string profileName)
        {
            var profile = await profileRepository.GetProfile(profileName);

            return View(profile);
        }

        public IActionResult FollowMember(string profileName)
        {
            profileRepository.ChangeFollowStatus(profileName, User.Identity.Name, true);

            return RedirectToAction("GetProfile", routeValues: new { profileName });
        }

        public IActionResult UnfollowMember(string profileName)
        {
            profileRepository.ChangeFollowStatus(profileName, User.Identity.Name, false);

            return RedirectToAction("GetProfile", routeValues: new { profileName });
        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}