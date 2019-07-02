using Eventures.Data.Seeding;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Eventures.Extensions
{
	public static class ApplicationBuilderExtensions
	{
		public static void UseDatabaseSeeding(this IApplicationBuilder app)
		{
			using (IServiceScope serviceScope = app.ApplicationServices.CreateScope())
			{
				IServiceProvider serviceProvider = serviceScope.ServiceProvider;

				List<ISeeder> seeders = new List<ISeeder>()
				{
					new RolesSeeder(),
					new ApplicationAdminSeeder()
				};

				foreach (var seeder in seeders)
				{
					seeder.Seed(serviceProvider).GetAwaiter().GetResult();
				}
			}
		}
	}
}
