using Microsoft.AspNetCore.Identity;

namespace ProjectUntitled.Models
{
    public interface IAccountRepository
    {
        void DeleteUser(IdentityUser user);
    }
}