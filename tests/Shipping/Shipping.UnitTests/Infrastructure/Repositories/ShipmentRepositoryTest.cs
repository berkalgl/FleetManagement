using Moq;
using Moq.EntityFrameworkCore;
using Shipping.Domain.AggregatesModel.ShipmentAggregate;
using Shipping.Infrastructure;
using Shipping.Infrastructure.Repositories;

namespace Shipping.UnitTests.Infrastructure.Repositories
{
    public class ShipmentRepositoryTest
    {
        [Fact]
        public async Task GetBy_ValidBarcode_ReturnsData()
        {
            //Arrange
            var data = new List<Shipment>
            {
                new Package("P7988000121", 1, 5),
            };

            var shipmentDbContext = new Mock<ShipmentDbContext>();
            shipmentDbContext.Setup(x => x.Shipments)
                .ReturnsDbSet(data);

            //Act
            var shipmentRepository = new ShipmentRepository(shipmentDbContext.Object);
            var result = await shipmentRepository.GetBy("P7988000121");

            //Assert
            Assert.NotNull(result);
        }
    }
}
