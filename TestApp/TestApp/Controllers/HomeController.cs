using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestApp.Models;

namespace TestApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IConfiguration configuration;

		public HomeController(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public IActionResult Index()
		{
			return this.View();
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
