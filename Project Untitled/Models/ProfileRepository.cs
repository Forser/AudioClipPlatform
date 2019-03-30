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
                                                                .Include(s => s.YouFollowing)
                                                                .Include(s => s.TheyFollowing)
                                                                .Include(s => s.Clips)
                                                                .FirstOrDefaultAsync();

                if(fetchedProfile != null)
                {
                    userProfile = Mapper.Map<UserProfileViewModel>(fetchedProfile);

                    userProfile.UserName = userIdentity.UserName;
                    userProfile.NumOfMembersYouFollow = fetchedProfile.YouFollowing.Count();
                    userProfile.NumberOfFollowers = fetchedProfile.TheyFollowing.Count();
                    userProfile.NumberOfPublishedClips = fetchedProfile.Clips.Count(s => s.FileStatus == FileStatus.Listed);
                }
            }

            return userProfile;
        }

        public void ChangeFollowStatus(string profileName, string loggedInUser, bool status)
        {
            Following following = new Following();
            var userFollowed = context.Users.Where(s => s.UserName == profileName).FirstOrDefault();
            var userFollowing = context.Users.Where(s => s.UserName == loggedInUser).FirstOrDefault();
            following.TheyFollowingId = userFollowed.Id;
            following.YouFollowId = userFollowing.Id;

            if (status)
            {
                context.Followings.Update(following);
            }
            else
            {
                var deleteFollow = context.Followings.Where(s => s.YouFollowId == userFollowing.Id && s.TheyFollowingId == userFollowed.Id).FirstOrDefault();

                context.Followings.Remove(deleteFollow);
            }

            context.SaveChanges();
        }
    }
}