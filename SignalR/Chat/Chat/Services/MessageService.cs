using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Data;
using Chat.Models.Message;

namespace Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext context;

        public MessageService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Create(string content, string userId, string recipientId)
        {
            var message = new Message
            {
                Content = content,
                RecipientId = recipientId,
                SenderId = userId
            };

            await context.AddAsync(message);
            await context.SaveChangesAsync();
        }

        public IEnumerable<MessageViewModel> UserMessagesByRecipientId(string userId, string recipientId)
        {
            return context.Messages
                .Where(x => x.SenderId == userId && x.RecipientId == recipientId)
                .OrderBy(x => x.SendOn)
                .Select(x => new MessageViewModel { Content = x.Content })
                .ToList();
        }
    }
}
