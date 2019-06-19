using FDMC.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FDMC.Data
{
    public class FdmcDbContext : DbContext
    {
        public FdmcDbContext() { }

        public FdmcDbContext(DbContextOptions<FdmcDbContext> options) : base(options)
        {
        }

        public DbSet<Cat> Cats { get; set; }
    }
}
