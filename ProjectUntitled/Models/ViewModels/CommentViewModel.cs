using System.ComponentModel.DataAnnotations;

namespace ProjectUntitled.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int ClipId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Don't forget to add a comment")]
        public string Message { get; set; }

        public string UserName { get; set; }
    }
}