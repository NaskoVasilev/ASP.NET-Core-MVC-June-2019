using JwtAuthentication.InputModels.Account;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Controllers
{
	public class AccountController : ApiController
	{
		private readonly UserManager<ApplicationUser> userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<ApplicationUser>> Register(RegisterInputModel model)
		{
			ApplicationUser user = new ApplicationUser
			{
				Email = model.Email,
				UserName = model.Username,
				FullName = model.FullName
			};

			var result = await userManager.CreateAsync(user);
			if(!result.Succeeded)
			{
				return BadRequest(result.Errors.Select(e => e.Description).ToList());
			}
			return user;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<ApplicationUser>> Login(LoginInputModel model)
		{
			return null;
		}
	}
}
