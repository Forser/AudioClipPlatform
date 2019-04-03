namespace ProjectUntitled.Models.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int ClipId { get; set; }

        public string Message { get; set; }

        public string UserName { get; set; }
    }
}