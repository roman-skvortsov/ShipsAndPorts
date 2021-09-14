using AutoMapper;
using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Core.Services;
using System.Collections.Generic;
using System.Linq;

namespace ShipsAndPorts.Services.Services
{
    /// <summary>
    /// Base service which contain CRUD operations
    /// </summary>
    /// <typeparam name="TViewModel">Api Model</typeparam>
    /// <typeparam name="TEntity">Db Model</typeparam>
    public class BaseService<TViewModel, TEntity> : IBaseService<TViewModel>
    {
        protected IBaseRepository<TEntity> repository;
        protected readonly IMapper mapper;

        public BaseService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public void SetRepository(IBaseRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public TViewModel Add(TViewModel record)
        {
            TEntity dto = mapper.Map<TEntity>(record);
            var newRecord = repository.Add(dto);
            return mapper.Map<TViewModel>(newRecord);
        }

        public bool Delete(int id)
        {
            return repository.Delete(id);
        }

        public TViewModel Get(int id)
        {
            return mapper.Map<TViewModel>(repository.Get(id));
        }

        public IEnumerable<TViewModel> GetAll()
        {
            return repository.GetAll().Select(rec => mapper.Map<TViewModel>(rec));
        }

        public bool Update(TViewModel record)
        {
            var dto = mapper.Map<TEntity>(record);
            return repository.Update(dto);
        }
    }
}
