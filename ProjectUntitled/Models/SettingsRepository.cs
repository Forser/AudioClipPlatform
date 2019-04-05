using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Blob;
using ProjectUntitled.Helpers;
using ProjectUntitled.Models.ViewModels;

namespace ProjectUntitled.Models
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly AppIdentityDbContext Context;
        private readonly IHostingEnvironment hostingEnviroment;
        private readonly AzureStorageConfig storageConfig = null;

        public SettingsRepository(AppIdentityDbContext ctx, IHostingEnvironment hostingEnviro, IOptions<AzureStorageConfig> config)
        {
            Context = ctx;
            hostingEnviroment = hostingEnviro;
            storageConfig = config.Value;
        }

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
            var profileImg = Context.ProfileImage.Where(w => w.OwnerId == user.Id).Select(s => s.ProfileImg).FirstOrDefault();

            var fetchedUser = Context.Users
                .Where(a => a.Id == user.Id)
                .Select(d => new { d.UserName, d.Email, ProfileImage = profileImg }).FirstOrDefault();

            if (fetchedUser != null)
            {
                User = Mapper.Map<UserViewModel>(fetchedUser);
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

        public async Task<bool> SaveFileInfo(FileUploadView fileUpload, IdentityUser user)
        {
            bool Succeeded = false;
            CloudBlockBlob blockBlob = null;

            var fileName = Guid.NewGuid() + ".mp3";

            if (fileName.EndsWith(".mp3"))
            {
                using (var stream = fileUpload.formFile.OpenReadStream())
                {
                    blockBlob = await StorageHelper.UploadFileToStorage(stream, fileName, storageConfig);
                    Succeeded = true;
                }

                var fileInfo = Mapper.Map<Clips>(fileUpload);

                fileInfo.FileName = blockBlob.Uri.AbsoluteUri;
                fileInfo.OwnerId = user.Id;
                fileInfo.UploadedBy = user.UserName;

                Context.Clips.Update(fileInfo);
                int x = await Context.SaveChangesAsync();
                if(x > 0) { Succeeded = true; }

            }
            return Succeeded;
        }

        public async Task<bool> SaveProfileImage(IFormFile fileUpload, IdentityUser user)
        {
            bool Succeeded = false;

            var profileImage = Context.ProfileImage.Where(s => s.OwnerId == user.Id).FirstOrDefault();

            var extension = Path.GetExtension(fileUpload.FileName);
            var fileName = Guid.NewGuid() + extension;
            var filePath = hostingEnviroment.WebRootPath + "\\img\\profileimg\\" + fileName;

            if(fileName.EndsWith(extension))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);
                }

                if (profileImage == null)
                {
                    profileImage = new ProfileImage();
                }

                profileImage.OwnerId = user.Id;
                profileImage.ProfileImg = fileName;

                Context.ProfileImage.Update(profileImage);
                int x = await Context.SaveChangesAsync();
                if(x > 0) { Succeeded = true; }
            }

            return Succeeded;
        }
    }
}
