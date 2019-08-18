using Chat.Models.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chat.Services
{
    public interface IMessageService
    {
        Task Create(string content, string userId, string recipientId);

        IEnumerable<MessageViewModel> UserMessagesByRecipientId(string userId, string recipientId);
    }
}
