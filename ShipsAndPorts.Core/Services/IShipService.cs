using ShipsAndPorts.Core.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsAndPorts.Core.Services
{
    public interface IShipService : IBaseService<ShipApiModel>
    {
        ClosestPortModel GetClosestPort(string shipId);
        void UpdateVelocity(string shipId, float velocity);
    }
}
