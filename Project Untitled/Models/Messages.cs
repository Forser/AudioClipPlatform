using System;

namespace Project_Untitled.Models
{
    public class Messages
    {
        public int Id { get; set; }

        public string SenderId { get; set; }
        public string SenderUserName { get; set; }
        public string RecipentId { get; set; }
        public string RecipentUserName { get; set; }

        public MessageStatus SenderStatus { get; set; } = MessageStatus.Sent;
        public MessageStatus RecipentStatus { get; set; } = MessageStatus.Unread;

        public string Title { get; set; }
        public string Message { get; set; }

        public DateTime ReadAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }

    public enum MessageStatus
    {
        Sent,
        Unread,
        Read,
        Replied,
        Deleted
    }
}