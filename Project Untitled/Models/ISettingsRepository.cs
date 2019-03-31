using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public interface ISettingsRepository
    {
        Task<bool> UpdateSettings(UserSettings userSettingsViewModel);

        Task<bool> UpdateNotifications(Notifications notificationsViewModel);

        UserViewModel GetUser(IdentityUser user);

        UserSettingsViewModel GetUserSettings(IdentityUser user);

        NotificationsViewModel GetUserNotifications(IdentityUser user);

        Task<bool> SaveFileInfo(FileUploadView fileUpload, IdentityUser user);
    }
}