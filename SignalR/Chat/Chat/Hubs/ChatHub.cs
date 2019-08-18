using Chat.Services;
using Ganss.XSS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Chat.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService messageService;
        private readonly IHtmlSanitizer htmlSanitizer;

        public ChatHub(IMessageService messageService, IHtmlSanitizer htmlSanitizer)
        {
            this.messageService = messageService;
            this.htmlSanitizer = htmlSanitizer;
        }

        public async Task SendMessage(string message, string recipientId)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            string sanitizedMessage = htmlSanitizer.Sanitize(message);
            string userId = Context.UserIdentifier;

            await messageService.Create(sanitizedMessage, userId, recipientId);

            await Clients.User(recipientId).SendAsync("ReceiveMessage", sanitizedMessage, userId);
            await Clients.Caller.SendAsync("SendedMessage", sanitizedMessage);
        }
    }
}
