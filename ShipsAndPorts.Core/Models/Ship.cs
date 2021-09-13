using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.Entity.Spatial;

namespace ShipsAndPorts.Core.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public string ShipId { get; set; }
        public string Name { get; set; }
        public float Velocity { get; set; }
        public DbGeography Geolocation { get; set; }
    }
}
