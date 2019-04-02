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
        public string RecipentUserName { get; set; }

        [DataType(DataType.Text)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}