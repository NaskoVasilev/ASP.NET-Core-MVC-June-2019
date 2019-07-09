using MessagesWebApi.Data.Models;
using MessagesWebApi.Models.InputModels.Message;
using MessagesWebApi.Models.ViewModels.Message;
using MessagesWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagesWebApi.Controllers
{
	public class MessagesController : ApiController
	{
		private readonly IMessageService messageService;
		private readonly UserManager<ApplicationUser> userManager;

		public MessagesController(IMessageService messageService, UserManager<ApplicationUser> userManager)
		{
			this.messageService = messageService;
			this.userManager = userManager;
		}		

		[Route("all")]
		public ActionResult<IEnumerable<MessageViewModel>> All()
		{
			return messageService.All();
		}

		[Authorize]
		[Route("create")]
		public async Task<ActionResult> Create(MessageCreateInputModel model)
		{
			ApplicationUser user = await userManager.GetUserAsync(this.User);
			Message message = await messageService.Create(model, user);
			MessageViewModel viewModel = new MessageViewModel { Content = message.Content, User = message.User.UserName };
			return Ok(viewModel);
		}
	}
}
