using Ganss.XSS;
using MessagesWebApi.Models.InputModels.Message;
using MessagesWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace MessagesWebApi.Hubs
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

        public async Task SendMessage(string content)
        {
            MessageCreateInputModel model = new MessageCreateInputModel { Content = htmlSanitizer.Sanitize(content) };
            string userId = Context.UserIdentifier;
            string username = Context.User.Identity.Name;
            await messageService.Create(model, userId);
            await Clients.All.SendAsync("ReceiveMessage", username, content);
        }
    } 
}
