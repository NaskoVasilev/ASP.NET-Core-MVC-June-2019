using JwtAuthentication.Configuration;
using JwtAuthentication.Extensions;
using JwtAuthentication.InputModels.Account;
using JwtAuthentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
				FullName = model.FullName,
			};

			var result = await userManager.CreateAsync(user, model.Password);
			if(!result.Succeeded)
			{
				return BadRequest(result.Errors.Select(e => e.Description).ToList());
			}
			return user;
		}

		[HttpPost("[action]")]
		public async Task<ActionResult<ApplicationUser>> Login(LoginInputModel model, [FromServices]IOptions<JwtSettings> settings)
		{
			var jwtToken = await userManager.Authenticate(model.Username, model.Password, settings.Value);

			if (jwtToken == null)
			{
				return BadRequest("Username or password is incorrect.");
			}

			return Ok(jwtToken);	
		}

		[Authorize]
		[HttpGet("username")]
		public ActionResult<string> GetUsername()
		{
			return this.User.Identity.Name;
		}
	}
}
