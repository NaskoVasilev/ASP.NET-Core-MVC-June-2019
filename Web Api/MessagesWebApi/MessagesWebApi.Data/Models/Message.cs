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

		[MaxLength(30)]
		[Required]
		public string User { get; set; }

		public DateTime CreatedOn { get; set; }
	}
}
