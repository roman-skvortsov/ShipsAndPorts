using ShipsAndPorts.Core.Models;
using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Repositories.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsAndPorts.Repositories.Repositories
{
    public class PortRepository : BaseRepository<Port>, IPortRepository
    {
        public PortRepository(ShipsAndPortsDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Returns the closest port
        /// </summary>
        /// <param name="point">Point where we are looking the closest port</param>
        /// <returns>The closest port or null </returns>
        public Port GetClosestPort(DbGeography point)
        {
            return DbContext.Ports
                .OrderBy(rec => rec.Geolocation.Distance(point))
                .FirstOrDefault();
        }

        public IList<Port> GetClosestPorts(DbGeography point, int? count)
        {
            var result = DbContext.Ports
                .OrderBy(rec => rec.Geolocation.Distance(point));
            if (count != null && count > 0)
            {
                return result.Take(count.Value).ToList();
            }

            return result.ToList();
        }
    }
}
