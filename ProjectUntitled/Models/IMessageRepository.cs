using Microsoft.AspNetCore.Identity;
using ProjectUntitled.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectUntitled.Models
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetMessages(IdentityUser user);

        Task<bool> SendMessage(Message message);

        Task<bool> ReplyMessage(GetMessageViewModel message, IdentityUser sender);

        GetMessageViewModel GetMessage(int messageId, string userId);

        void ChangeMessageStatus(int messageId, string userId, MessageStatus status);

        bool AllowMessages(string userName);
    }
}
