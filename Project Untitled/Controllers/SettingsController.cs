using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Untitled.Models;
using Project_Untitled.Models.ViewModels;
using System.Threading.Tasks;

namespace Project_Untitled.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsRepository repository;
        private UserManager<IdentityUser> userManager;

        public SettingsController(ISettingsRepository repo, UserManager<IdentityUser> userMgr)
        {
            repository = repo;
            userManager = userMgr;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            UserViewModel userViewModel = repository.GetUser(user);

            return View(userViewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSettings(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var Succeeded = await repository.UpdateSettings(userViewModel, user);

                if (Succeeded)
                {
                    TempData["message"] = "Your settings has been updated";
                    return RedirectToAction("Index", "Settings", "user-home");
                }
                else
                {
                    ModelState.AddModelError("", "Could not update the settings!");
                    return View("Index", userViewModel);
                }
            }
            ModelState.AddModelError("", "Error : Could not update the settings!");
            return View("Index", userViewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNotifications(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var Succeeded = await repository.UpdateNotifications(userViewModel, user);

                if (Succeeded)
                {
                    TempData["message"] = "Your notifications has been updated";
                    return RedirectToAction("Index", "Settings", "user-notify");
                }
                else
                {
                    ModelState.AddModelError("", "Could not update your notifications!");
                    return View("Index", userViewModel);
                }
            }
            ModelState.AddModelError("", "Error: Could not update your notifications!");
            return View("Index", userViewModel);
        }
    }
}