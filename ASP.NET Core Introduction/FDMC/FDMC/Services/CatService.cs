using FDMC.Data;
using FDMC.Data.Models;
using FDMC.Services.Contracts;
using System.Linq;

namespace FDMC.Services
{
    public class CatService : IService<Cat>
    {
        private readonly FdmcDbContext context;

        public CatService(FdmcDbContext context)
        {
            this.context = context;
        }

        public void Add(Cat entity)
        {
            this.context.Cats.Add(entity);
            context.SaveChanges();

        }

        public IQueryable<Cat> All()
        {
            return context.Cats.AsQueryable<Cat>();
        }

        public Cat GetById(int id)
        {
            Cat cat = context.Cats.Find(id);
            return cat;
        }
    }
}
