using MediatR;
using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Events
{
    public record ShipmentUnloadFailedDomainEvent : INotification
    {
        public Shipment Shipment { get; }
        public int FailedDeliveryPoint { get; }
        public ShipmentUnloadFailedDomainEvent(Shipment shipment, int failedDeliveryPoint)
        {
            Shipment = shipment;
            FailedDeliveryPoint = failedDeliveryPoint;
        }
    }
}
