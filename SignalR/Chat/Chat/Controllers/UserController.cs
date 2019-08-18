using Chat.Models.User;
using Chat.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Chat.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMessageService messageService;

        public UserController(UserManager<IdentityUser> userManager, IMessageService messageService)
        {
            this.userManager = userManager;
            this.messageService = messageService;
        }

        public IActionResult Chat(string recipientId)
        {
            string userId = userManager.GetUserId(this.User);
            string username = this.User.Identity.Name;
            string recipient = userManager.Users.FirstOrDefault(x => x.Id == recipientId)?.UserName;

            var friends = userManager.Users
               .Where(x => x.Id != userId)
               .Select(x => new UserFriendViewModel
               {
                   Id = x.Id,
                   Username = x.UserName
               })
               .ToList();

            var chatViewModel = new ChatViewModel
            {
                Messages = messageService.UserMessagesByRecipientId(userId, recipientId),
                Recipient = recipient,
                RecipientId = recipientId,
                Sender = username,
                SenderId = userId,
                Friends = friends
            };

            return View(chatViewModel);
        }
    }
}
