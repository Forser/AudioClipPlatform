using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter a username with at least 2 characters.")]
        [StringLength(60, MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Remote("IsUserNameAvailable", "Account", HttpMethod = "POST", ErrorMessage = "Username already exists. Please enter a different user name")]
        public string Name { get; set; }

        [Display(Name = "Password")]
        [UIHint("password")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,30}$", ErrorMessage = "Password is required with at least 6 characters, 1 number and both lower and uppercase letters. ")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare(otherProperty: "Password", ErrorMessage = "Password & Confirm Password does not match!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,30}$", ErrorMessage = "Password is required with at least 6 characters, 1 number and both lower and uppercase letters. ")]
        [UIHint("password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        [Remote("IsEmailAlreadyRegistered", "Account", HttpMethod = "POST", ErrorMessage = "Email already registered. Please login, do a password reset or use another email address")]
        public string Email { get; set; }

        public string ReturnUrl { get; set; } = "/";

    }
}