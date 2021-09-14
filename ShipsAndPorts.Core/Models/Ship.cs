using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShipsAndPorts.Core.Models
{
    public class Ship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string ShipId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float Velocity { get; set; }
        [Required]
        public DbGeography Geolocation { get; set; }
    }
}
