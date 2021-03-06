﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagesWebApi.Data;
using MessagesWebApi.Data.Models;
using MessagesWebApi.Models.InputModels.Message;
using MessagesWebApi.Models.ViewModels.Message;

namespace MessagesWebApi.Services
{
	public class MessageService : IMessageService
	{
		private readonly ApplicationDbContext context;

		public MessageService(ApplicationDbContext context)
		{
			this.context = context;
		}

		public List<MessageViewModel> All()
		{
			return context.Messages
				.OrderBy(m => m.CreatedOn)
				.Select(m => new MessageViewModel() { Content = m.Content, User = m.User.UserName })
				.ToList();
		}

		public async Task<Message> Create(MessageCreateInputModel model, string userId)
		{
			Message message = new Message { UserId = userId, Content = model.Content };
			await context.Messages.AddAsync(message);
			await context.SaveChangesAsync();
			return message;
		}
	}
}
