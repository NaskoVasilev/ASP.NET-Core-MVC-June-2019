using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Eventures.Data.Seeding
{
	public class RolesSeeder : ISeeder
	{
		public async Task Seed(IServiceProvider serviceProvider)
		{
			RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			await SeedRole("User", roleManager);
			await SeedRole("Admin", roleManager);
		}

		private async Task SeedRole(string roleName, RoleManager<IdentityRole> roleManager)
		{
			if(!await roleManager.RoleExistsAsync(roleName))
			{
				await roleManager.CreateAsync(new IdentityRole { Name = roleName });
			}
		}
	}
}
