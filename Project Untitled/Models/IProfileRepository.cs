using ProjectUntitled.Models.ViewModels;
using System.Threading.Tasks;

namespace ProjectUntitled.Models
{
    public interface IProfileRepository
    {
        Task<UserProfileViewModel> GetProfile(string profileName);

        void ChangeFollowStatus(string profileName, string loggedInUser, bool status);
    }
}