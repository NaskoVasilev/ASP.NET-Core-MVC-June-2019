using MessagesWebApi.Data.Models;
using MessagesWebApi.Models.InputModels.Message;
using MessagesWebApi.Models.ViewModels.Message;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessagesWebApi.Services
{
	public interface IMessageService
	{
		List<MessageViewModel> All();

		Task<Message> Create(MessageCreateInputModel model, string userId);
	}
}
