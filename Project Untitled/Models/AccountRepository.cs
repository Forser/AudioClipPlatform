using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace ProjectUntitled.Models
{
    public class AccountRepository : IAccountRepository
    {
        private AppIdentityDbContext context;

        public AccountRepository(AppIdentityDbContext ctx) { context = ctx; }

        public void DeleteUser(IdentityUser user)
        {
            context.UserSettings.RemoveRange(context.UserSettings.Where(a => a.OwnerId == user.Id));
        }
    }
}