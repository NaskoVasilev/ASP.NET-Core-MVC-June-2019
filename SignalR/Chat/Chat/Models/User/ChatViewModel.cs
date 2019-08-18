using Chat.Models.Message;
using System.Collections.Generic;

namespace Chat.Models.User
{
    public class ChatViewModel
    {
        public string SenderId { get; set; }

        public string Sender { get; set; }

        public string RecipientId { get; set; }

        public string Recipient { get; set; }

        public IEnumerable<MessageViewModel> Messages { get; set; }

        public IEnumerable<UserFriendViewModel> Friends { get; set; }
    }
}
