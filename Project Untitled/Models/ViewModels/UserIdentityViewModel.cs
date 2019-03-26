using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Project_Untitled.Models.ViewModels
{
    public class UserIdentityViewModel
    {
        [BindRequired]
        public UserHandler User { get; set; }

        [BindRequired]
        public IdentityUser Identity { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}