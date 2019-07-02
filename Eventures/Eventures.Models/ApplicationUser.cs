using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
	public class ApplicationUser : IdentityUser<string>
	{
		[Required]
		[StringLength(30, MinimumLength = 3)]
		public string FirstName { get; set; }

		[Required]
		[StringLength(30, MinimumLength = 3)]
		public string LastName { get; set; }

		[Required]
		[StringLength(10, MinimumLength = 2)]
		public string UniqueCitizenNumber { get; set; }
	}
}
