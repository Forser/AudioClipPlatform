namespace Project_Untitled.Models.ViewModels
{
    public class GetMessageViewModel
    {
        public Messages Message { get; set; }

        public Messages NewMessage { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}