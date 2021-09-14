using ShipsAndPorts.Core.Models;
using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Repositories.DatabaseContext;
using System.Linq;

namespace ShipsAndPorts.Repositories.Repositories
{
    public class ShipRepository : BaseRepository<Ship>, IShipRepository
    {
        public ShipRepository(ShipsAndPortsDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Returns ship by ship id
        /// </summary>
        /// <param name="shipId">Ship Id</param>
        /// <returns>Ship</returns>
        public Ship GetByShipId(string shipId)
        {
            return DbContext.Ships
                .FirstOrDefault(rec => rec.ShipId == shipId);
        }

        /// <summary>
        /// Update ship velocity
        /// </summary>
        /// <param name="shipId">Ship id</param>
        /// <param name="velocity">Velocity</param>
        public void UpdateVelocity(string shipId, float velocity)
        {
            var rec = DbContext.Ships
                .FirstOrDefault(rec => rec.ShipId == shipId);

            // if there is a ship with the id
            if (rec != null)
            {
                rec.Velocity = velocity;
                DbContext.Update(rec);
                DbContext.SaveChanges();
            }
        }
    }
}
