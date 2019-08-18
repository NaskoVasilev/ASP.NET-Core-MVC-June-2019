using Microsoft.AspNetCore.Identity;
using System;

namespace Chat.Data
{
    public class Message
    {
        public Message()
        {
            SendOn = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }
        public IdentityUser Sender { get; set; }

        public string RecipientId { get; set; }
        public IdentityUser Recipient { get; set; }

        public DateTime SendOn { get; set; }
    }
}
