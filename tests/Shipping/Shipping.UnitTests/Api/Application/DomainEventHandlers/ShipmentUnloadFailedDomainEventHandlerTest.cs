
using Moq;
using Shipping.API.Application.Commands;
using Shipping.API.Application.DomainEventHandlers;
using Shipping.Domain.AggregatesModel.ShipmentAggregate;
using Shipping.Domain.Events;
using Shipping.Infrastructure.Logging.Services;

namespace Shipping.UnitTests.Api.Application.DomainEventHandlers
{
    public class ShipmentUnloadFailedDomainEventHandlerTest
    {
        private readonly Mock<ILogService> _logServiceMock;
        public ShipmentUnloadFailedDomainEventHandlerTest()
        {
            _logServiceMock = new Mock<ILogService>();
        }
        [Fact]
        public async Task ShipmentUnloadFailedDomainEvent_Handled_Success()
        {
            //Arrange
            var fakeShipmentUnloadFailedDomainEvent = GetFakeShipmentUnloadFailedDomainEvent();
            var cltToken = default(CancellationToken);

            _logServiceMock
                .Setup(_logService => _logService.Log(It.IsAny<string>()))
                .Returns(Task.CompletedTask);

            //Act
            var handler = new ShipmentUnloadFailedDomainEventHandler(_logServiceMock.Object);
            await handler.Handle(fakeShipmentUnloadFailedDomainEvent, cltToken);

            //Assert
            _logServiceMock.Verify(logService => logService.Log(It.IsAny<string>()), Times.Once);


        }
        private static ShipmentUnloadFailedDomainEvent GetFakeShipmentUnloadFailedDomainEvent()
        {
            return new ShipmentUnloadFailedDomainEvent(new Package("P7988000121", 1, 5), 2);
        }
    }
}
