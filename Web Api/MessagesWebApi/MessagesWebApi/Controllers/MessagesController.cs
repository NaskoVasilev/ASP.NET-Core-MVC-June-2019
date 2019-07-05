using MessagesWebApi.Models.InputModels.Message;
using MessagesWebApi.Models.ViewModels.Message;
using MessagesWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagesWebApi.Controllers
{
	public class MessagesController : ApiController
	{
		private readonly IMessageService messageService;

		public MessagesController(IMessageService messageService)
		{
			this.messageService = messageService;
		}		

		[Route("all")]
		public ActionResult<IEnumerable<MessageViewModel>> All()
		{
			return messageService.All();
		}

		[Route("create")]
		public async Task<ActionResult> Create(MessageCreateInputModel model)
		{
			await messageService.Create(model);
			return Ok();
		}
	}
}
