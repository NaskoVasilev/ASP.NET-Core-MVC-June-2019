using System.ComponentModel.DataAnnotations;

namespace MessagesWebApi.Models.InputModels.Account
{
	public class RegisterInputModel
	{
		private const int UsernameMinLength = 5;
		private const int PasswordMinLength = 5;

		[Required]
		[MinLength(UsernameMinLength)]
		public string Username { get; set; }


		[MinLength(PasswordMinLength)]
		[Required]
		public string Password { get; set; }
	}
}
