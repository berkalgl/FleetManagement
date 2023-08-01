using Microsoft.Extensions.Logging;
using Moq;
using Shipping.API.Application.Commands;
using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.UnitTests.Api.Application.Commands
{
    public class DistributeCommandHandlerTest
    {
        private readonly Mock<IShipmentRepository> _shipmentRepositoryMock;
        private readonly Mock<ILogger<DistributeCommandHandler>> _loggerMock;
        public DistributeCommandHandlerTest()
        {
            _shipmentRepositoryMock = new Mock<IShipmentRepository>();
            _loggerMock = new Mock<ILogger<DistributeCommandHandler>>();
        }
        [Fact]
        public async Task DistributeCommand_NotEmptyRequest_Handled()
        {
            //Arrange
            var fakeDistributeCommand = GetFakeDistributeCommand();
            var cltToken = default(CancellationToken);
            var barcode = "P7988000121";

            _shipmentRepositoryMock
                .Setup(shipmentRepository => shipmentRepository.GetBy(barcode))
                .ReturnsAsync(GetFakeShipmentEntity());

            _shipmentRepositoryMock.Setup(shipmentRepository => shipmentRepository.UnitOfWork.SaveEntitiesAsync(cltToken))
                .Returns(Task.FromResult(true));

            //Act
            var handler = new DistributeCommandHandler(_shipmentRepositoryMock.Object, _loggerMock.Object);
            var result = await handler.Handle(fakeDistributeCommand, cltToken);

            //Assert
            Assert.True(result.Route.Count > 0);
        }
        [Fact]
        public async Task DistributeCommand_NotEmptyRequest_NotExist_ReturnsEmpty()
        {
            //Arrange
            var fakeDistributeCommand = GetFakeDistributeCommand();
            var cltToken = default(CancellationToken);
            var barcode = "P7988000121";

            _shipmentRepositoryMock
                .Setup(shipmentRepository => shipmentRepository.GetBy(barcode))
                .ReturnsAsync((Shipment)null);

            _shipmentRepositoryMock.Setup(shipmentRepository => shipmentRepository.UnitOfWork.SaveEntitiesAsync(cltToken))
                .Returns(Task.FromResult(true));

            //Act
            var handler = new DistributeCommandHandler(_shipmentRepositoryMock.Object, _loggerMock.Object);
            var result = await handler.Handle(fakeDistributeCommand, cltToken);

            //Assert
            Assert.True(result.Route.FirstOrDefault()?.Deliveries.Count == 0);
        }
        private static DistributeCommand GetFakeDistributeCommand()
        {
            var distributeCommand = new DistributeCommand()
            {
                VehiclePlate = "34TL34",
                Routes = new List<Route>()
                {
                    new Route()
                    {
                        DeliveryPoint = 1,
                        Deliveries = new List<Delivery>()
                        {
                            new Delivery()
                            {
                                Barcode = "P7988000121"
                            }
                        }

                    }
                }
            };

            return distributeCommand;
        }
        private static Shipment GetFakeShipmentEntity()
        {
            return new Package("P7988000121", 1, 5);
        }
    }
}
