using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using TestApp.Models;

namespace TestApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IConfiguration configuration;
		private readonly UserManager<IdentityUser> userManager;
        private readonly ILogger<HomeController> logger;
        private readonly IMemoryCache memoryCache;

        public HomeController(IConfiguration configuration, UserManager<IdentityUser> userManager, 
            ILogger<HomeController> logger, IMemoryCache memoryCache)
		{
			this.configuration = configuration;
			this.userManager = userManager;
            this.logger = logger;
            this.memoryCache = memoryCache;
        }

        
		public IActionResult Index(string name)
		{
			return View();
		}

		[Authorize]
		public async Task<IActionResult> AddCountryClaim(string country)
		{
			Claim claim = new Claim(ClaimTypes.Country, country);
			var currentUser = await userManager.GetUserAsync(this.User);
			var result = await userManager.AddClaimAsync(currentUser, claim);
			if (result.Succeeded)
			{
				return Ok("The clam was added");
			}
			else
			{
				string errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
				return BadRequest(errorMessage);
			}
		}

		[Authorize]
		public async Task<IActionResult> GetClaims()
		{
			var currentUser = await userManager.GetUserAsync(this.User);
			IList<Claim> claims = await userManager.GetClaimsAsync(currentUser);
			string claimsAsText = string.Join(Environment.NewLine, claims.Select(c => $"{c.Type} -> {c.Value} -> {c.Subject}"));
			return Ok(claimsAsText);
		}

		[Authorize(Policy = "CountryOrigin")]
		public IActionResult GetUserCountry()
		{
			Claim claim = this.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Country);
			return Ok(claim.Value);	
		}

		public IActionResult Create()
		{
			return this.View();
		}

		[HttpPost]
		public IActionResult Create(HomeInputModel model)
		{
			if(!ModelState.IsValid)
			{
				return View(model);
			}

			return Json(model);
		}

		public IActionResult Privacy()
		{
			ViewBag.People = new List<IdentityUser>() { new IdentityUser{ UserName = "Atanas", Email = "nasko.it.admin@dev.bg" } };

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
