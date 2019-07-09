using System.ComponentModel.DataAnnotations;

namespace MessagesWebApi.Models.InputModels.Message
{
	public class MessageCreateInputModel
	{
		[Required]
		public string Content { get; set; }
	}
}
