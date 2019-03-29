using System.Collections.Generic;

namespace Project_Untitled.Models.ViewModels
{
    public class MessagesViewModel
    {
        public MessagesViewModel()
        {
            NewMessage = new NewMessageViewModel();
        }

        public IEnumerable<MessageViewModel> Messages { get; set; }
        public NewMessageViewModel NewMessage { get; set; }
    }
}