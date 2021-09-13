
using System.Data.Entity.Spatial;

namespace ShipsAndPorts.Core.Models
{
    public class Port
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DbGeography Geolocation { get; set; }

    }
}
