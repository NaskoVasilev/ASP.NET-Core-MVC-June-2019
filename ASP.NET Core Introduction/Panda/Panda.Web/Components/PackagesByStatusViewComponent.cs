using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Panda.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DeliveryApplication.Components
{
	[ViewComponent(Name = "PackagesByStatus")]
    public class PackagesByStatusViewComponent : ViewComponent
    {
        private readonly IPackageService service;
        private readonly UserManager<IdentityUser> userManager;

        public PackagesByStatusViewComponent(IPackageService service, UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
            this.service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync(string status)
        {
            var userId = userManager.GetUserId(this.User as ClaimsPrincipal);
			ViewData["status"] = status;
            var packages = service.GetPackagesByStatus(status, userId);
            return View(packages);
        }
    }
}
