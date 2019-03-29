using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;

namespace Project_Untitled.Models
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly AppIdentityDbContext _context;

        public SettingsRepository(AppIdentityDbContext ctx) { _context = ctx; }

        public UserSettingsViewModel Settings { get; set; }
        public NotificationsViewModel Notifications { get; set; }
        public UserViewModel User { get; set; }

        public UserSettingsViewModel GetUserSettings(IdentityUser user)
        {
            var _user = _context.UserHandler.Where(a => a.IdentityId == user.Id).Select(d => d.Id).FirstOrDefault();
            if (_user != null)
            {
                var _settings = _context.UserSettings.SingleOrDefault(c => c.UserId == _user);

                if (_settings != null)
                {
                    Settings = Mapper.Map<UserSettings, UserSettingsViewModel>(_settings);
                }
                else
                {
                    Settings = new UserSettingsViewModel();
                }

                Settings.UserId = _user.Value;
            }
            return Settings;
        }

        public NotificationsViewModel GetUserNotifications(IdentityUser user)
        {
            var _user = _context.UserHandler.Where(a => a.IdentityId == user.Id).Select(d => d.Id).FirstOrDefault();
            if(_user != null)
            {
                var _notifications = _context.Notifications.SingleOrDefault(c => c.UserId == _user);
                if( _notifications != null)
                {
                    Notifications = Mapper.Map<Notifications, NotificationsViewModel>(_notifications);
                }
                else
                {
                    Notifications = new NotificationsViewModel();
                }

                Notifications.UserId = _user.Value;
            }

            return Notifications;
        }

        public UserViewModel GetUser(IdentityUser user)
        {
            var _user = _context.Users.Where(a => a.Id == user.Id).Select(d => new { d.UserName, d.Email }).FirstOrDefault();
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

            _context.UserSettings.Update(userSettings);
            int x = await _context.SaveChangesAsync();

            if (x > 0) { Succeeded = true; }

            return Succeeded;
        }

        public async Task<bool> UpdateNotifications(Notifications notificationsViewModel)
        {
            bool Succeeded = false;

            _context.Notifications.Update(notificationsViewModel);
            int x = await _context.SaveChangesAsync();

            if (x > 0) { Succeeded = true; }

            return Succeeded;
        }
    }
}
