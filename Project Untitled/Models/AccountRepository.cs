using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Project_Untitled.Models
{
    public class AccountRepository : IAccountRepository
    {
        private AppIdentityDbContext _context;

        public AccountRepository(AppIdentityDbContext ctx) { _context = ctx; }

        public void DeleteUser(IdentityUser user)
        {
            _context.UserSettings.RemoveRange(_context.UserSettings.Where(a => a.OwnerId == user.Id));
        }
    }
}