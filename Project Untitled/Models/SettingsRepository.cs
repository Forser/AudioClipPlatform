using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;

namespace Project_Untitled.Models
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly AppIdentityDbContext Context;

        public SettingsRepository(AppIdentityDbContext ctx) { Context = ctx; }

        public UserSettingsViewModel Settings { get; set; }
        public NotificationsViewModel Notifications { get; set; }
        public UserViewModel User { get; set; }

        public UserSettingsViewModel GetUserSettings(IdentityUser user)
        {
            var User = Context.UserSettings.Where(a => a.OwnerId == user.Id).FirstOrDefault();

            if (User != null)
            {
                Settings = Mapper.Map<UserSettings, UserSettingsViewModel>(User);
            }
            else
            {
                Settings = new UserSettingsViewModel();
            }

            return Settings;
        }

        public NotificationsViewModel GetUserNotifications(IdentityUser user)
        {
            var userNotifications = Context.Notifications.Where(a => a.OwnerId == user.Id).FirstOrDefault();
            if (userNotifications != null)
            {
                Notifications = Mapper.Map<Notifications, NotificationsViewModel>(userNotifications);
            }
            else
            {
                Notifications = new NotificationsViewModel();
            }

            return Notifications;
        }

        public UserViewModel GetUser(IdentityUser user)
        {
            var _user = Context.Users.Where(a => a.Id == user.Id).Select(d => new { d.UserName, d.Email }).FirstOrDefault();
            if(_user != null)
            {
                User = Mapper.Map<UserViewModel>(_user);
            }
            else
            {
                User = new UserViewModel();
            }

            return User;
        }

        public async Task<bool> UpdateSettings(UserSettings userSettings)
        {
            var Succeeded = false;

            Context.UserSettings.Update(userSettings);
            int x = await Context.SaveChangesAsync();

            if (x > 0) { Succeeded = true; }

            return Succeeded;
        }

        public async Task<bool> UpdateNotifications(Notifications notificationsViewModel)
        {
            bool Succeeded = false;

            Context.Notifications.Update(notificationsViewModel);
            int x = await Context.SaveChangesAsync();

            if (x > 0) { Succeeded = true; }

            return Succeeded;
        }
    }
}
