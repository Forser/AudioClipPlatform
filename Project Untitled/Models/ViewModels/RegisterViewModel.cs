using System.ComponentModel.DataAnnotations;

namespace Project_Untitled.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The username is required")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [UIHint("password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(otherProperty: "Password", ErrorMessage = "Password & Confirm Password does not match!")]
        [UIHint("password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public string ReturnUrl { get; set; } = "/";

    }
}