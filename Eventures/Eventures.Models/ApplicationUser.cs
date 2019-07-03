using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
	public class ApplicationUser : IdentityUser<string>
	{
		[Required]
		[MaxLength(30)]
		public string FirstName { get; set; }

		[Required]
		[MaxLength(30)]
		public string LastName { get; set; }

		[Required]
		[MaxLength(10)]
		public string UniqueCitizenNumber { get; set; }
	}
}
