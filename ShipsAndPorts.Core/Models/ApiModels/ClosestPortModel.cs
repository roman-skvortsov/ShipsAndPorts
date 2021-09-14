using System;

namespace ShipsAndPorts.Core.Models.ApiModels
{
    public class ClosestPortModel
    {
        public DateTime ArrivalTime { get; set; }
        public double Distance { get; set; }
        public PortApiModel PortDetails { get; set; }
        public ShipApiModel ShipDetails { get; set; }
    }
}
