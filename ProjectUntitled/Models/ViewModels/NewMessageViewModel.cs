using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class NewMessageViewModel
    {
        public string SenderId { get; set; }
        public string SenderUserName { get; set; }

        public string RecipentId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Requires a Username", AllowEmptyStrings = false)]
        [Remote("IsUserRegistered", "Account", HttpMethod = "POST", ErrorMessage = "Must be a valid Username as Recipent")]
        public string RecipentUserName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Enter a Subject for the message", AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Dont forget to add a message!", AllowEmptyStrings = false)]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}