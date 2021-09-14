using System.Data.Entity.Spatial;

namespace ShipsAndPorts.Core.Models.ApiModels
{
    public class ShipApiModel
    {
        public int Id { get; set; }
        public string ShipId { get; set; }
        public string Name { get; set; }
        public float Velocity { get; set; }
        public DbGeography Geolocation { get; set; }
    }
}
