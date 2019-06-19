using System.Linq;

namespace FDMC.Services.Contracts
{
    public interface IService<T>
    {
        IQueryable<T> All();

        void Add(T entity);

        T GetById(int id);
    }
}
