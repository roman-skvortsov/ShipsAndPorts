using ShipsAndPorts.Core.Models;
using System.Collections.Generic;
using System.Data.Entity.Spatial;

namespace ShipsAndPorts.Core.Repositories
{
    public interface IPortRepository : IBaseRepository<Port>
    {
        public IList<Port> GetClosestPorts(DbGeography point, int? count);
        public Port GetClosestPort(DbGeography point);
    }
}
