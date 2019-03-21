using System.ComponentModel.DataAnnotations;

namespace Project_Untitled.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
