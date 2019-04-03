using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectUntitled.Models;
using ProjectUntitled.Models.ViewModels;
using System.Threading.Tasks;

namespace ProjectUntitled.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsRepository settingsRepository;
        private readonly IClipsRepository clipsRepository;
        private readonly UserManager<IdentityUser> userManager;

        public SettingsController(ISettingsRepository settingsRepo, UserManager<IdentityUser> usrManager, IClipsRepository clipsRepo)
        {
            settingsRepository = settingsRepo;
            clipsRepository = clipsRepo;
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
                var Succeeded = await settingsRepository.UpdateSettings(userSettings);

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
                var Succeeded = await settingsRepository.UpdateNotifications(userNotifications);

                if (Succeeded)
                {
                    TempData["message"] = "Your notifications has been updated";
                    return RedirectToAction("Index", "Settings", "user-notify");
                }
            }

            ModelState.AddModelError("", "Error: Could not update your notifications!");
            return View("Index", notificationsViewModel);
        }

        public IActionResult UploadAudio()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadAudio([FromForm]FileUploadView fileUpload)
        {
            if(ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                var Succeeded = await settingsRepository.SaveFileInfo(fileUpload, currentUser);

                if(Succeeded)
                {
                    TempData["message"] = "Your audioclip has been uploaded!";
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Error: Could not upload your clip!");
            return View("UploadAudio", fileUpload);
        }

        public IActionResult UploadProfileImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadProfileImage([FromForm]IFormFile fileUpload)
        {
            if(ModelState.IsValid)
            {
                var currentUser = await userManager.GetUserAsync(HttpContext.User);
                var Succeeded = await settingsRepository.SaveProfileImage(fileUpload, currentUser);

                if(Succeeded)
                {
                    TempData["message"] = "Your profile image has been uploaded!";
                    return RedirectToAction("Index", "Settings");
                }
            }

            ModelState.AddModelError("", "Error: Could not upload your image");
            return View("UploadProfileImage", fileUpload);
        }

        public async Task<IActionResult> PublishClip(int id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var changedStatus = await clipsRepository.ChangeClipStatus(id, user, FileStatus.Listed);
            
            if(changedStatus)
            {
                TempData["message"] = "Your clip has been published!";
                return RedirectToAction("Index", "Settings");
            }

            return View();
        }

        public async Task<IActionResult> UnpublishClip(int id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var changedStatus = await clipsRepository.ChangeClipStatus(id, user, FileStatus.Unlisted);

            if (changedStatus)
            {
                TempData["message"] = "Your clip has been unpublished!";
                return RedirectToAction("Index", "Settings");
            }

            return View();
        }

        public async Task<IActionResult> DeleteClip(int id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var clipDeleted = await clipsRepository.DeleteClip(id, user);

            if(clipDeleted)
            {
                TempData["message"] = "Your clip has been deleted!";
                return RedirectToAction("Index", "Settings");
            }

            return View();
        }
    }
}