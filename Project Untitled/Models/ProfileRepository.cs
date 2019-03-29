using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project_Untitled.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppIdentityDbContext _context;

        public ProfileRepository(AppIdentityDbContext context) { _context = context; }

        public async Task<UserProfileViewModel> GetProfile(string profileName)
        {
            UserProfileViewModel userProfile = new UserProfileViewModel();

            var _user = _context.Users.Where(a => a.UserName == profileName).Select(s => new { s.Id, s.UserName }).FirstOrDefault();

            if(_user != null)
            {
                var _available = await _context.UserHandler.Where(s => s.IdentityId == _user.Id).FirstOrDefaultAsync();

                if(_available != null)
                {
                    var _userHandler = _context.UserHandler;
                    var _userSettings = _context.UserSettings;

                    var _fullQuery = _userHandler.Join(_userSettings,
                        p => p.Id, x => x.UserId,
                        (userHandle, userSet) => new { userHandle, userSet });

                    userProfile.FollowsTotal = _context.Followings.Where(s => s.FollowingId == _user.Id)
                                            .Select(d => d.FollowingId).Count();

                    userProfile.NumberOfFollowers = _context.Followings.Where(s => s.FollowerId == _user.Id)
                                            .Select(d => d.FollowerId).Count();

                    userProfile.NumberOfPublishedClips = _context.Clips.Where(s => s.UserProfile.UserId == _available.Id 
                        && s.FileStatus == FileStatus.Listed)
                        .Select(d => d.FileName).Count();

                    var queryResult = _fullQuery.Select(sel => new
                    {
                        sel.userSet.DateOfBirth,
                        sel.userSet.Gender,
                        sel.userSet.Biography,
                        sel.userSet.Twitter,
                        sel.userSet.Facebook,
                        sel.userSet.Instagram,
                        sel.userSet.Tumblr,
                        sel.userSet.Reddit,
                        sel.userSet.Twitch,
                        sel.userSet.Mixer,
                        sel.userSet.Youtube,
                        sel.userSet.Periscope,
                        sel.userSet.Livestream,
                        sel.userSet.Spotify,
                        sel.userSet.Wordpress,
                        _user.UserName,
                        userProfile.NumberOfPublishedClips,
                        userProfile.FollowsTotal,
                        userProfile.NumberOfFollowers
                    }).FirstOrDefault();

                    userProfile = Mapper.Map<UserProfileViewModel>(queryResult);

                }
            }

            return userProfile;
        }
    }
}