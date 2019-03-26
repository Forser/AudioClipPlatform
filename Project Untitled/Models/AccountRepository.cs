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
            _context.UserHandler.RemoveRange(_context.UserHandler.Where(a => a.IdentityId == user.Id));
            _context.Notifications.RemoveRange(_context.Notifications.Where(a => a.IdentityId == user.Id));
        }
    }
}