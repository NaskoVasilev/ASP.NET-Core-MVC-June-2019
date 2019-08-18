using Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;

        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public async Task SendMessage(string message, string recipientId)
        {
            string userId = Context.UserIdentifier;
            string username = Context.User.Identity.Name;

            await messageService.Create(message, userId, recipientId);

            await Clients.User(recipientId).SendAsync("ReceiveMessage", message, userId);
        }
    }
}
