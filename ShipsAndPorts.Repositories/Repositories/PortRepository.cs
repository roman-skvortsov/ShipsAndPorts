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
    public class PortRepository : BaseRepository<Port>, IPortRepository
    {
        public PortRepository(ShipsAndPortsDbContext dbContext) : base(dbContext)
        {
        }
    }
}
