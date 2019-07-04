using CarsApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsApi.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Car> Cars { get; set; }
	}
}
