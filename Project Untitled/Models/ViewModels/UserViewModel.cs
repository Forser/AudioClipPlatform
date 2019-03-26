using Microsoft.AspNetCore.Identity;

namespace Project_Untitled.Models.ViewModels
{
    public class UserViewModel
    {
        public UserHandler User { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}