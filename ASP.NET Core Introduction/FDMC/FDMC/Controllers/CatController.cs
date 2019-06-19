using FDMC.Data.Models;
using FDMC.Services.Contracts;
using FDMC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FDMC.Controllers
{
    public class CatController : Controller
    {
        private readonly IService<Cat> service;

        public CatController(IService<Cat> service)
        {
            this.service = service;
        }

        [HttpGet("/cats/add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("/cats/add")]
        public IActionResult Create(CatCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
				return this.View(model);
            }

            Cat cat = new Cat(model.Name, model.Age, model.Breed, model.ImageUrl);
            service.Add(cat);
            return this.RedirectToAction("Index", "Home");
        }

        [HttpGet("/cats/{id}")]
        public IActionResult Details(int id)
        {
            Cat cat = service.GetById(id);

            if(cat == null)
            {
                this.RedirectToAction("Home", "Index");
            }

            CatViewModel model = new CatViewModel(cat.Name, cat.Age, cat.Breed, cat.ImageUrl);
            return View(model);
        }
    }
}