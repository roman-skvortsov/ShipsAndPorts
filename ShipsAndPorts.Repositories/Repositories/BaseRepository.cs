using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Repositories.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsAndPorts.Repositories.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public ShipsAndPortsDbContext DbContext { get; }

        public BaseRepository(ShipsAndPortsDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public bool Delete(int id)
        {
            var recToDelete = DbContext.Find<TEntity>(id);
            if (recToDelete != null)
            {
                DbContext.Remove<TEntity>(recToDelete);
                DbContext.SaveChanges();
            }
            return true;
        }

        public TEntity Get(int id)
        {
            return DbContext.Find<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>();
        }

        public TEntity Add(TEntity record)
        {
            if (record == null)
            {
                return null;
            }

            DbContext.Add<TEntity>(record);
            DbContext.SaveChanges();

            return record;
        }

        public bool Update(TEntity record)
        {
            if (record == null)
            {
                return false;
            }

            DbContext.Update(record);
            DbContext.SaveChanges();

            return true;
        }
    }
}
