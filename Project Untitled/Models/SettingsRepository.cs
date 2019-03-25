using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Project_Untitled.Models.ViewModels;

namespace Project_Untitled.Models
{
    public class SettingsRepository : ISettingsRepository
    {
        private AppIdentityDbContext context;
        ILogger<ISettingsRepository> logger;

        public SettingsRepository(AppIdentityDbContext ctx, ILogger<ISettingsRepository> logger) { context = ctx; this.logger = logger; }

        public UserViewModel GetUser(IdentityUser user)
        {
            UserViewModel userViewModel = new UserViewModel();

            var _user = context.UserHandler.FirstOrDefault(t => t.IdentityUser == user);
            var _notifications = context.Notifications.Where(t => t.IdentityUser == user) as Notifications;

            if (_user == null)
            {
                userViewModel.IdentityUser = user;
                return userViewModel;
            }
            else if (_user != null && _notifications == null)
            {
                userViewModel.User = _user;
                userViewModel.User.Notifications = new Notifications();
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
                userViewModel.User.IdentityUser = user;
                context.UserHandler.Update(userViewModel.User);
                await context.SaveChangesAsync();
                Succeeded = true;
                return Succeeded;
            }

            return Succeeded;
        }

        public async Task<bool> UpdateNotifications(UserViewModel userViewModel)
        {
            bool Succeeded = false;

            if (userViewModel != null)
            {
                context.Notifications.Update(userViewModel.User.Notifications);
                await context.SaveChangesAsync();
                Succeeded = true;
                return Succeeded;
            }

            return Succeeded;
        }
    }
}
