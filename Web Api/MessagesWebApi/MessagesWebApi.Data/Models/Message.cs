using System;
using System.ComponentModel.DataAnnotations;

namespace MessagesWebApi.Data.Models
{
	public class Message
	{
		public Message()
		{
			this.CreatedOn = DateTime.UtcNow;
		}

		public string Id { get; set; }

		[Required]
		public string Content { get; set; }

		[Required]
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}
