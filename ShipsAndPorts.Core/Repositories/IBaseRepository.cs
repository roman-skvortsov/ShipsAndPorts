using System.Linq;

namespace ShipsAndPorts.Core.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        TEntity Get(int id);
        bool Update(TEntity record);
        TEntity Add(TEntity record);
        bool Delete(int id);
        IQueryable<TEntity> GetAll();
    }
}
