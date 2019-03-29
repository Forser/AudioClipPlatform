using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        public SettingsController(ISettingsRepository repo)
        {
            repository = repo;
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
                var _userSettings = Mapper.Map<UserSettings>(userSettingsViewModel);
                var Succeeded = await repository.UpdateSettings(_userSettings);

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
                var _notifications = Mapper.Map<Notifications>(notificationsViewModel);
                var Succeeded = await repository.UpdateNotifications(_notifications);

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