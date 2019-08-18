using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>(messageBuilder =>
            {
                messageBuilder.HasOne(x => x.Sender)
                .WithMany()
                .HasForeignKey(x => x.SenderId);

                messageBuilder.HasOne(x => x.Recipient)
               .WithMany()
               .HasForeignKey(x => x.RecipientId);
            });
        }
    }
}
