using ShipsAndPorts.Core.Models;
using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Repositories.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsAndPorts.Repositories.Repositories
{
    public class ShipRepository : BaseRepository<Ship>, IShipRepository
    {
        public ShipRepository(ShipsAndPortsDbContext dbContext): base(dbContext)
        {
        }
    }
}
