using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Panda.InputModels.Package;
using Panda.Models.Enums;
using Panda.Services;

namespace DeliveryApplication.Controllers
{
	public class PackageController : Controller
    {
        private readonly IPackageService packageService;

        public PackageController(IPackageService packageService)
        {
            this.packageService = packageService;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["Users"] = packageService.GetAllRecipients();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(PackageCreateInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Users"] = packageService.GetAllRecipients();
                return View(model);
            }

            packageService.Create(model);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var model = packageService.GetById(id);
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Pending()
        {
            var packages = packageService.PackagesByStatus(PackageStatus.Pending);
            return View(packages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Shipped()
        {
            var packages = packageService.PackagesByStatus(PackageStatus.Shipped);
            return View(packages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delivered()
        {
			var packages = packageService.GetDeliveredPackages();
            return View(packages);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Ship(int id)
        {
            packageService.SetDeliveyDate(id);
            packageService.UpdateStatus(id, PackageStatus.Shipped);
            return RedirectToAction(nameof(Pending));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Deliver(int id)
        {
            packageService.UpdateStatus(id, PackageStatus.Delivered);
            return RedirectToAction(nameof(Shipped));
        }

        [Authorize]
        public IActionResult Acquire(int id, int recipientId)
        {
            packageService.UpdateStatus(id, PackageStatus.Acquired);
            packageService.GenerateReceipt(id, recipientId);
            return RedirectToAction("Index", "Home");
        }
    }
}