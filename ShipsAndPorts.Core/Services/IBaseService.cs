using System.Collections.Generic;

namespace ShipsAndPorts.Core.Services
{
    public interface IBaseService<TViewModel>
    {
        TViewModel Get(int id);
        bool Update(TViewModel record);
        TViewModel Add(TViewModel record);
        bool Delete(int id);
        IEnumerable<TViewModel> GetAll();
    }
}
