using Microsoft.EntityFrameworkCore;
using ShipsAndPorts.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsAndPorts.Repositories.DatabaseContext
{
    public class ShipsAndPortsDbContext : DbContext
    {
        public ShipsAndPortsDbContext(DbContextOptions<ShipsAndPortsDbContext> options)
               : base(options)
        {
        }

        public DbSet<Ship> Ships { get; set; }
        public DbSet<Port> Ports { get; set; }
    }
}
