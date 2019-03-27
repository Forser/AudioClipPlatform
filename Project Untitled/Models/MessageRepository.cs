using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Project_Untitled.Models.ViewModels;

namespace Project_Untitled.Models
{
    public class MessageRepository : IMessageRepository
    {
        private AppIdentityDbContext context;

        public MessageRepository(AppIdentityDbContext ctx)
        {
            context = ctx;
        }

        public void ChangeMessageStatus(int messageId, string userId, MessageStatus status)
        {
            var message = context.Messages.First(r => r.Id == messageId);

            if(message.RecipentId == userId)
            {
                message.RecipentStatus = status;
            }
            else if (message.SenderId == userId)
            {
                message.SenderStatus = status;
            }

            context.Messages.Update(message);
            context.SaveChangesAsync();
        }

        public IList<Messages> GetMessages(IdentityUser user)
        {
            var messages = context.Messages.Where(r => r.RecipentId == user.Id && r.RecipentStatus != MessageStatus.Deleted)
                .OrderBy(o => o.CreatedAt).ToList();

            return messages;
        }

        public GetMessageViewModel GetMessage(int messageId, string userId)
        {
            GetMessageViewModel messages = new GetMessageViewModel();

            messages.Message = context.Messages.FirstOrDefault(i => i.Id == messageId && i.RecipentStatus != MessageStatus.Deleted);

            messages.Message.ReadAt = DateTime.Now;
            messages.Message.RecipentStatus = MessageStatus.Read;
            context.Messages.Update(messages.Message);
            context.SaveChangesAsync();

            return messages;
        }

        public async Task<bool> SendMessage(MessagesViewModel message)
        {
            bool Succeeded = false;

            var _recipient = context.Users.Where(r => r.UserName == message.NewMessage.RecipentUserName).Select(s => new { s.UserName, s.Id }).FirstOrDefault();
            var _sender = context.Users.Where(r => r.Id == message.User.Id).Select(s => new { s.UserName }).FirstOrDefault();

            if (_recipient != null)
            {
                message.NewMessage.CreatedAt = DateTime.Now;
                message.NewMessage.SenderId = message.User.Id;
                message.NewMessage.SenderUserName = _sender.UserName;
                message.NewMessage.RecipentUserName = _recipient.UserName;
                message.NewMessage.RecipentId = _recipient.Id;
                context.Messages.Update(message.NewMessage);
                var result = await context.SaveChangesAsync();
                Succeeded = true;
                return Succeeded;
            }

            return Succeeded;
        }

        public async Task<bool> ReplyMessage(GetMessageViewModel message)
        {
            bool Succeeded = false;

            var _sender = context.Users.Where(c => c.Id == message.Message.RecipentId).Select(s => new { s.Id, s.UserName }).FirstOrDefault();
            var _recipent = context.Users.Where(c => c.Id == message.Message.SenderId).Select(s => new { s.Id, s.UserName }).FirstOrDefault();

            message.NewMessage.CreatedAt = DateTime.Now;
            message.NewMessage.SenderId = _sender.Id;
            message.NewMessage.SenderUserName = _sender.UserName;
            message.NewMessage.RecipentId = _recipent.Id;
            message.NewMessage.RecipentUserName = _recipent.UserName;
            message.NewMessage.Title = "RE: " + message.Message.Title;
            context.Messages.Update(message.NewMessage);
            var result = await context.SaveChangesAsync();

            var _message = context.Messages.FirstOrDefault(i => i.Id == message.Message.Id);
            _message.RecipentStatus = MessageStatus.Replied;
            context.Messages.Update(_message);
            await context.SaveChangesAsync();

            if(result != 0)
            {
                Succeeded = true;
            }

            return Succeeded;
        }
    }
}