using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public interface ISettingsRepository
    {
        Task<bool> UpdateSettings(UserViewModel userViewModel, IdentityUser user);
        UserViewModel GetUser(IdentityUser user);
    }
}