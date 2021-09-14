using System.Data.Entity.Spatial;

namespace ShipsAndPorts.Core.Models.ApiModels
{
    public class PortApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DbGeography Geolocation { get; set; }
    }
}
