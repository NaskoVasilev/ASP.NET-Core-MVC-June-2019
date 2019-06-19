using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Panda.Web.Seeders
{
	public class Seeder
	{
		private readonly RoleManager<IdentityRole> roleManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IConfiguration configuration;

		public Seeder(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
		{
			this.roleManager = roleManager;
			this.userManager = userManager;
			this.configuration = configuration;
		}

		public void SeedNeededRoles()
		{
			string[] roles = configuration["Roles"].Split(", ");
			foreach (var role in roles)
			{
				SeedRole(role);
			}
		}

		public void SeedRole(string roleName)
		{
			var roleExists = roleManager.RoleExistsAsync(roleName).Result;
			if (!roleExists)
			{
				IdentityRole role = new IdentityRole(roleName);
				IdentityResult result = roleManager.CreateAsync(role).Result;
			}
		}

		public void SeedAdminUser()
		{
			string username = "admin";
			string email = "admin@gmail.com";
			string password = "admin123";
			var targetUser = userManager.FindByNameAsync(username).Result;
			if (targetUser == null)
			{
				IdentityUser user = new IdentityUser() { UserName = username, Email = email };
				var userResult = userManager.CreateAsync(user, password).Result;
				var userRoleResult = userManager.AddToRoleAsync(user, "Admin").Result;
			}
		}
	}
}
