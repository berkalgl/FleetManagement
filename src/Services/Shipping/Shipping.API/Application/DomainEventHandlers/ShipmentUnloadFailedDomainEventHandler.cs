using MediatR;
using Shipping.Domain.Events;
using Shipping.Infrastructure.Logging.Services;
using System.Text;

namespace Shipping.API.Application.DomainEventHandlers
{
    public class ShipmentUnloadFailedDomainEventHandler : INotificationHandler<ShipmentUnloadFailedDomainEvent>
    {
        private readonly ILogService _logService;
        public ShipmentUnloadFailedDomainEventHandler(ILogService logService)
        {
            _logService = logService;
        }
        public async Task Handle(ShipmentUnloadFailedDomainEvent notification, CancellationToken cancellationToken)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"The package with the barcode {notification.Shipment.Barcode} ");
            stringBuilder.Append($"could not be unloaded to delivery point with the id {notification.FailedDeliveryPoint}");
            var messageToLog = stringBuilder.ToString();

            await _logService.Log(messageToLog);
        }
    }
}
