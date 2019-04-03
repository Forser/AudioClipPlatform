using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ProjectUntitled.Models.ViewModels;
using System.Threading.Tasks;

namespace ProjectUntitled.Models
{
    public interface ISettingsRepository
    {
        Task<bool> UpdateSettings(UserSettings userSettingsViewModel);

        Task<bool> UpdateNotifications(Notifications notificationsViewModel);

        UserViewModel GetUser(IdentityUser user);

        UserSettingsViewModel GetUserSettings(IdentityUser user);

        NotificationsViewModel GetUserNotifications(IdentityUser user);

        Task<bool> SaveFileInfo(FileUploadView fileUpload, IdentityUser user);

        Task<bool> SaveProfileImage(IFormFile fileUpload, IdentityUser user);
    }
}