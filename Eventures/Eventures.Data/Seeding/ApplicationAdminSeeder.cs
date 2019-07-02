using Eventures.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Eventures.Data.Seeding
{
	public class ApplicationAdminSeeder : ISeeder
	{
		public async Task Seed(IServiceProvider serviceProvider)
		{
			UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			if(await userManager.FindByNameAsync("admin") == null)
			{
				ApplicationUser admin = new ApplicationUser
				{
					FirstName = "Admin",
					LastName = "Admin",
					UserName = "admin",
					UniqueCitizenNumber = "1234567890",
					Email = "admin@mail.com"
				};

				await userManager.CreateAsync(admin, "admin");
				await userManager.AddToRoleAsync(admin, "Admin");
			}
		}
	}
}
