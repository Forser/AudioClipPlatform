using Microsoft.AspNetCore.Identity;

namespace Project_Untitled.Models
{
    public interface IAccountRepository
    {
        void DeleteUser(IdentityUser user);
    }
}