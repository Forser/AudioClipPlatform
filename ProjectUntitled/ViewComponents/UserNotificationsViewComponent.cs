﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectUntitled.Models;
using System.Threading.Tasks;

namespace ProjectUntitled.ViewComponents
{
    [Authorize]
    public class UserNotificationsViewComponent : ViewComponent
    {
        private readonly ISettingsRepository repository;
        private UserManager<IdentityUser> userManager;

        public UserNotificationsViewComponent(ISettingsRepository repo, UserManager<IdentityUser> usrManager)
        {
            repository = repo;
            userManager = usrManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var userNotificationsViewModel = repository.GetUserNotifications(user);

            return View(userNotificationsViewModel);
        }
    }
}