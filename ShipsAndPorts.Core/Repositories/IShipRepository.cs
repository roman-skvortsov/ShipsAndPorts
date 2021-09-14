using ShipsAndPorts.Core.Models;

namespace ShipsAndPorts.Core.Repositories
{
    public interface IShipRepository : IBaseRepository<Ship>
    {
        public void UpdateVelocity(string shipId, float velocity);
        public Ship GetByShipId(string shipId);
    }
}
