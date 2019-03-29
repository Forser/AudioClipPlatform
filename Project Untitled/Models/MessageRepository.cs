using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        public IEnumerable<Message> GetMessages(IdentityUser user)
        {
            var _messages = context.Messages.Where(r => r.RecipentId == user.Id && r.RecipentStatus != MessageStatus.Deleted)
                .OrderBy(o => o.CreatedAt).ToList();

            return _messages;
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

        public async Task<bool> SendMessage(Message message)
        {
            bool Succeeded = false;
            message.CreatedAt = DateTime.Now;

            context.Messages.Update(message);
            var result = await context.SaveChangesAsync();
            if (result > 0) { Succeeded = true; }

            return Succeeded;
        }

        public async Task<bool> ReplyMessage(GetMessageViewModel replyMessage, IdentityUser _sender)
        {
            bool Succeeded = false;

            var _recipent = context.Users.Where(c => c.Id == replyMessage.Message.SenderId).Select(s => new { s.Id, s.UserName }).FirstOrDefault();

            replyMessage.NewMessage.RecipentId = _recipent.Id;
            replyMessage.NewMessage.RecipentUserName = _recipent.UserName;
            replyMessage.NewMessage.CreatedAt = DateTime.Now;
            replyMessage.NewMessage.SenderId = _sender.Id;
            replyMessage.NewMessage.SenderUserName = _sender.UserName;
            replyMessage.NewMessage.Title = "RE: " + replyMessage.Message.Title;

            context.Messages.Update(replyMessage.NewMessage);
            var result = await context.SaveChangesAsync();

            var _message = context.Messages.FirstOrDefault(i => i.Id == replyMessage.Message.Id);
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