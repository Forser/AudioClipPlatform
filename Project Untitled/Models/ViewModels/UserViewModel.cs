using System.ComponentModel.DataAnnotations;

namespace Project_Untitled.Models.ViewModels
{
    public class UserViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string UserName { get; set; }
    }
}