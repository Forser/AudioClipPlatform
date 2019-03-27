using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_Untitled.Models
{
    public interface IMessageRepository
    {
        IList<Messages> GetMessages(IdentityUser user);

        Task<bool> SendMessage(MessagesViewModel message);

        Task<bool> ReplyMessage(GetMessageViewModel message);

        GetMessageViewModel GetMessage(int messageId, string userId);

        void ChangeMessageStatus(int messageId, string userId, MessageStatus status);
    }
}
