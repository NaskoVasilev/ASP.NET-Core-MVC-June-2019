using System.ComponentModel.DataAnnotations;

namespace MessagesWebApi.Models.InputModels.Account
{
	public class RegisterInputModel
	{
		private const int UsernameMinLength = 5;
		private const int FullNameMinLength = 7;
		private const int PasswordMinLength = 5;

		[Required]
		[MinLength(UsernameMinLength)]
		public string Username { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[MinLength(FullNameMinLength)]
		public string FullName { get; set; }

		[Compare(nameof(ConfirmPassword))]
		[MinLength(PasswordMinLength)]
		[Required]
		public string Password { get; set; }

		public string ConfirmPassword { get; set; }
	}
}
