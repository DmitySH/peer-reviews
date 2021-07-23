using System.Collections.Generic;

namespace WebShet.Models
{
    public class Message
    {
        public static List<Message> Messages = new List<Message>();

        public Message(string subject, string senderId, string receiverId, string content)
        {
            Subject = subject;
            SenderId = senderId;
            ReceiverId = receiverId;
            Content = content;
        }

        public string Subject { get; }
        public string ReceiverId { get; }
        public string SenderId { get; }
        public string Content { get; }

    }
}
