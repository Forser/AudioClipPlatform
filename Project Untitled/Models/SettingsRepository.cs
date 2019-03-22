using System;
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
            var _user = context.UserHandler.FirstOrDefault(t => t.OwnerId == user.Id);
            UserViewModel userViewModel = new UserViewModel();

            if (_user == null)
            {
                return userViewModel;
            }

            userViewModel.User = _user;

            return userViewModel;
        }

        public async Task<bool> UpdateSettings(UserViewModel userViewModel, IdentityUser user)
        {
            bool Succeeded = false;

            if (userViewModel != null)
            {
                userViewModel.User.OwnerId = user.Id;
                context.UserHandler.Update(userViewModel.User);
                logger.LogDebug("Update was called");
                await context.SaveChangesAsync();
                logger.LogDebug("SaveChangesAsync was called");
                Succeeded = true;
                return Succeeded;
            }

            return Succeeded;
        }
    }
}
