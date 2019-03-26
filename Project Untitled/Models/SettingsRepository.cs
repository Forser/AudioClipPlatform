using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;

namespace Project_Untitled.Models
{
    public class SettingsRepository : ISettingsRepository
    {
        private AppIdentityDbContext context;

        public SettingsRepository(AppIdentityDbContext ctx) { context = ctx; }

        public UserViewModel GetUser(IdentityUser user)
        {
            UserViewModel userViewModel = new UserViewModel();

            var _user = context.UserHandler.FirstOrDefault(t => t.IdentityId == user.Id);
            var _notifications = context.Notifications.FirstOrDefault(t => t.IdentityId == user.Id) as Notifications;
            userViewModel.IdentityUser = user;

            if (_user == null && _notifications == null)
            {
                userViewModel.User = new UserHandler();
                userViewModel.User.Notifications = new Notifications();
                return userViewModel;
            }
            else if (_user != null && _notifications == null)
            {
                userViewModel.User = _user;
                userViewModel.User.Notifications = new Notifications();
                userViewModel.User.Notifications.UserId = _user.Id;
                return userViewModel;
            }
            else if (_user == null && _notifications != null)
            {
                userViewModel.User = new UserHandler();
                userViewModel.User.Notifications = _notifications;
                return userViewModel;
            }

            userViewModel.User = _user;
            userViewModel.User.Notifications = _notifications;

            return userViewModel;
        }

        public async Task<bool> UpdateSettings(UserViewModel userViewModel, IdentityUser user)
        {
            bool Succeeded = false;

            if (userViewModel != null)
            {
                userViewModel.User.IdentityId = user.Id;
                context.UserHandler.Update(userViewModel.User);
                await context.SaveChangesAsync();
                Succeeded = true;
                return Succeeded;
            }

            return Succeeded;
        }

        public async Task<bool> UpdateNotifications(UserViewModel userViewModel, IdentityUser user)
        {
            bool Succeeded = false;

            if (userViewModel != null)
            {
                userViewModel.User.Notifications.UserId = userViewModel.User.Id;
                userViewModel.User.Notifications.IdentityId = user.Id;
                context.Notifications.Update(userViewModel.User.Notifications);
                await context.SaveChangesAsync();
                Succeeded = true;
                return Succeeded;
            }

            return Succeeded;
        }
    }
}
