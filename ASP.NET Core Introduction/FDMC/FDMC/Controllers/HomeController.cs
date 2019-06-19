using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FDMC.ViewModels;
using FDMC.Services.Contracts;
using FDMC.Data.Models;

namespace FDMC.Controllers
{
	public class HomeController : Controller
    {
        private readonly IService<Cat> service;

        public HomeController(IService<Cat> service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            CatCommonViewModel[] cats = service.All()
                .Select(c => new CatCommonViewModel() { Id = c.Id, Name = c.Name })
                .ToArray();
            return View(cats);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
