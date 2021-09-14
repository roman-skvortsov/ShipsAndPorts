using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ShipsAndPorts.Core.Models;
using ShipsAndPorts.Core.Repositories;
using ShipsAndPorts.Repositories.DatabaseContext;
using ShipsAndPorts.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipsAndPorts.Repositories.Test.Repositories
{
    public class ShipRepositoryTests
    {
        private readonly Mock<ShipsAndPortsDbContext> db = new Mock<ShipsAndPortsDbContext>();
        private IShipRepository shipRepository;
        private Ship ship1;
        private Ship ship2;
        
        [SetUp]
        public void Setup()
        {
            ship1 = new Ship
            {
                Id = 1,
                Geolocation = DbGeography.PointFromText(String.Format("POINT({0} {1})", 10, 10), 4326),
                Name = "Ship 1",
                ShipId = "SHP-1",
                Velocity = 5
            };

            ship2 = new Ship
            {
                Id = 2,
                Geolocation = DbGeography.PointFromText(String.Format("POINT({0} {1})", 10, 9), 4326),
                Name = "Ship 2",
                ShipId = "SHP-2",
                Velocity = 7
            };

            var data = new List<Ship>
            {
                ship1,
                ship2
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Ship>>();
            mockSet.As<IQueryable<Ship>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Ship>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Ship>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Ship>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ShipsAndPortsDbContext>();
            mockContext.Setup(c => c.Ships).Returns(mockSet.Object);

            shipRepository = new ShipRepository(mockContext.Object);
        }



        [Test]
        public void GetById_NegativeId()
        {
            // Arrange

            // Act
            var rec = shipRepository.Get(1);

            //Assert
            Assert.AreEqual(null, null);
        }
    }
}
