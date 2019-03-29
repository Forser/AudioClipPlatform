using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project_Untitled.Models;
using System.Threading.Tasks;

namespace Project_Untitled.ViewComponents
{
    [Authorize]
    public class UserNotificationsViewComponent : ViewComponent
    {
        private readonly ISettingsRepository _repository;
        private UserManager<IdentityUser> _userManager;

        public UserNotificationsViewComponent(ISettingsRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var _user = await _userManager.GetUserAsync(HttpContext.User);
            var _userNotificationsViewModel = _repository.GetUserNotifications(_user);

            return View(_userNotificationsViewModel);
        }
    }
}