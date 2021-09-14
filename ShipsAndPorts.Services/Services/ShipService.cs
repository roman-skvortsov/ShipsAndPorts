using AutoMapper;
using ShipsAndPorts.Core.Models;
using ShipsAndPorts.Core.Models.ApiModels;
using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Core.Services;
using System;

namespace ShipsAndPorts.Services.Services
{
    public class ShipService : BaseService<Ship, ShipApiModel>, IShipService
    {
        private readonly IShipRepository shipRepository;
        private readonly IPortRepository portRepository;

        public ShipService(IShipRepository shipRepository, 
            IPortRepository portRepository,
            IMapper mapper) : base(mapper)
        {
            this.shipRepository = shipRepository;
            this.portRepository = portRepository;
        }
        
        /// <summary>
        /// Returns the closest port to the ship and qrrival time ship
        /// </summary>
        /// <param name="shipId">Ship Identifier</param>
        /// <returns>Data about the closest port</returns>
        public ClosestPortModel GetClosestPort(string shipId)
        {
            var result = new ClosestPortModel();
            var ship = shipRepository.GetByShipId(shipId);
            if (ship != null)
            {
                var port = portRepository.GetClosestPort(ship.Geolocation);
                if(port!=null)
                {
                    result.Distance = port.Geolocation.Distance(ship.Geolocation).Value;
                    // Time in hours = distance / speed
                    result.ArrivalTime = DateTime.UtcNow.AddHours(result.Distance / ship.Velocity);
                    result.PortDetails = mapper.Map<PortApiModel>(port);
                    result.ShipDetails = mapper.Map<ShipApiModel>(ship);
                }
            }

            return result;
        }

        public void UpdateVelocity(string shipId, float velocity)
        {
            shipRepository.UpdateVelocity(shipId, velocity);
        }
    }
}
