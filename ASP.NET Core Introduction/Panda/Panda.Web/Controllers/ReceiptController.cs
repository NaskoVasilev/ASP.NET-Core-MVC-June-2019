using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Services;

namespace DeliveryApplication.Controllers
{
	public class ReceiptController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IReceiptService service;

        public ReceiptController(UserManager<IdentityUser> userManager, IReceiptService service)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [Authorize]
        public IActionResult UserReceipts()
        {
            string id = userManager.GetUserId(this.User);
            var receipts = service.GetRecepitsById(id);
            return View(receipts);
        }

        [Authorize]
        public IActionResult Details(int id)
        {
            var receipt = service.GetReceiptById(id);
            return View(receipt);
        }
    }
}