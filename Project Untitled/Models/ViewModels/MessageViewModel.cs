namespace ProjectUntitled.Models.ViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string RecipentId { get; set; }
        public string RecipentUserName { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
    }
}
