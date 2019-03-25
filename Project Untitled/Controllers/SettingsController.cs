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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewSettings()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            UserViewModel userViewModel = repository.GetUser(user);

            if (userViewModel.User != null)
            {
                return View(userViewModel);
            }

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ViewSettings(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                var Succeeded = await repository.UpdateSettings(userViewModel, user);

                if (Succeeded)
                {
                    TempData["message"] = "Your settings has been updated";
                    return View();
                }
                else
                {
                    ModelState.AddModelError("", "Could not update the settings!");
                    return View(userViewModel);
                }
            }
            ModelState.AddModelError("", "Error : Could not update the settings!");
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNotifications(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                userViewModel.User.Notifications.UserId = userViewModel.User.Id;
                var Succeeded = await repository.UpdateNotifications(userViewModel);

                if (Succeeded)
                {
                    TempData["message"] = "Your notifications has been updated";
                    return RedirectToAction("ViewSettings");
                }
                else
                {
                    ModelState.AddModelError("", "Could not update your notifications!");
                    return View(userViewModel);
                }
            }
            ModelState.AddModelError("", "Error: Could not update your notifications!");
            return View(userViewModel);
        }

        public IActionResult ViewNotificationSettings()
        {
            return View();
        }
    }
}