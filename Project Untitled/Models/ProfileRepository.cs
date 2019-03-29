using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_Untitled.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppIdentityDbContext context;

        public ProfileRepository(AppIdentityDbContext ctx) { context = ctx; }

        public async Task<UserProfileViewModel> GetProfile(string profileName)
        {
            UserProfileViewModel userProfile = new UserProfileViewModel();

            var userIdentity = await context.Users.Where(a => a.UserName == profileName).Select(s => new { s.Id, s.UserName }).FirstOrDefaultAsync();

            if (userIdentity != null)
            {
                var fetchedProfile = await context.UserSettings.Where(s => s.OwnerId == userIdentity.Id)
                                                                .Include(s => s.Following).Include(s => s.Clips)
                                                                .FirstOrDefaultAsync();

                if(fetchedProfile != null)
                {
                    userProfile = Mapper.Map<UserProfileViewModel>(fetchedProfile);

                    userProfile.UserName = userIdentity.UserName;
                    userProfile.NumOfMembersYouFollow = fetchedProfile.Following.Select(s => s.OwnerId == fetchedProfile.OwnerId).Count();
                    userProfile.NumberOfFollowers = fetchedProfile.Following.Select(s => s.FollowerId == fetchedProfile.OwnerId).Count();
                    userProfile.NumberOfPublishedClips = fetchedProfile.Clips.Where(s => s.OwnerId == fetchedProfile.OwnerId
                                                                                 && s.FileStatus == FileStatus.Listed).Count();
                }
            }

            return userProfile;
        }
    }
}