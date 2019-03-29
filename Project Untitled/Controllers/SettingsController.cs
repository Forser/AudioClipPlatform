using AutoMapper;
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
        private readonly UserManager<IdentityUser> userManager;

        public SettingsController(ISettingsRepository repo, UserManager<IdentityUser> usrManager)
        {
            repository = repo;
            userManager = usrManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSettings([FromForm] UserSettingsViewModel userSettingsViewModel)
        {
            if(ModelState.IsValid)
            {
                var userSettings = Mapper.Map<UserSettings>(userSettingsViewModel);
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                userSettings.OwnerId = currentUser.Id;
                var Succeeded = await repository.UpdateSettings(userSettings);

                if (Succeeded)
                {
                    TempData["message"] = "Your settings has been updated";
                    return RedirectToAction("Index", "Settings", "user-home");
                }
            }

            ModelState.AddModelError("", "Error : Could not update the settings!");
            return View("Index", userSettingsViewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateNotifications([FromForm] NotificationsViewModel notificationsViewModel)
        {
            if(ModelState.IsValid)
            {
                var userNotifications = Mapper.Map<Notifications>(notificationsViewModel);
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                userNotifications.OwnerId = currentUser.Id;
                var Succeeded = await repository.UpdateNotifications(userNotifications);

                if (Succeeded)
                {
                    TempData["message"] = "Your notifications has been updated";
                    return RedirectToAction("Index", "Settings", "user-notify");
                }
            }

            ModelState.AddModelError("", "Error: Could not update your notifications!");
            return View("Index", notificationsViewModel);
        }
    }
}