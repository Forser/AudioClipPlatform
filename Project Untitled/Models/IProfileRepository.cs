using Project_Untitled.Models.ViewModels;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public interface IProfileRepository
    {
        Task<UserProfileViewModel> GetProfile(string profileName);
    }
}