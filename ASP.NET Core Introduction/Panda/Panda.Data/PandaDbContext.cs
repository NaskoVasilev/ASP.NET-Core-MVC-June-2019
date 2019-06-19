using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Panda.Models;

namespace Panda.Web.Data
{
	public class PandaDbContext : IdentityDbContext
	{
		public PandaDbContext(DbContextOptions<PandaDbContext> options)
			: base(options)
		{
		}

		public DbSet<Package> Packages { get; set; }

		public DbSet<Receipt> Receipts { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Receipt>()
				.HasOne(r => r.Package)
				.WithOne(p => p.Receipt)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Receipt>()
				.HasOne(r => r.Recipient)
				.WithMany()
				.HasForeignKey(r => r.RecipientId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Package>()
				.HasOne(r => r.Recipient)
				.WithMany()
				.HasForeignKey(r => r.RecipientId)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(builder);
		}
	}
}
