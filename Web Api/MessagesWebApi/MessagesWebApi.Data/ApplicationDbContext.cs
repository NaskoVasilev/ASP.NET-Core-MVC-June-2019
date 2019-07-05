using MessagesWebApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagesWebApi.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Message> Messages { get; set; }
	}
}
