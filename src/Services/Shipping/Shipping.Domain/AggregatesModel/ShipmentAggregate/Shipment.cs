using System.ComponentModel.DataAnnotations;
using Shipping.Domain.Events;
using Shipping.Domain.Policies.Rules.ShipmentDeliveryRules;
using Shipping.Domain.SeedWork;

namespace Shipping.Domain.AggregatesModel.ShipmentAggregate
{
    public abstract class Shipment : Entity, IAggregateRoot
    {
        [Key]
        public string Barcode { get; set; }
        public int DeliveryPointId { get; set; }
        public DeliveryPoint DeliveryPoint => DeliveryPoint.From(DeliveryPointId);
        public Shipment(string barcode, int deliveryPoint)
        {
            Barcode = barcode;
            DeliveryPointId = deliveryPoint;
        }
        public bool CanUnload(int deliveryPoint)
        {
            var targetDeliveryPoint = DeliveryPoint.From(deliveryPoint);
            var result = targetDeliveryPoint.GetDeliveryPointRule().CanUnloadShipment(this);

            if(!result)
                AddShipmentUnloadFailedDomainEvent(deliveryPoint);

            return result;
        }
        public abstract Shipment Load();
        public abstract Shipment Unload(int deliveryPoint);
        public abstract Shipment Deliver(int deliveryPoint);
        public abstract IShipmentDeliveryRule GetShipmentDeliveryRule();
        void AddShipmentUnloadFailedDomainEvent(int failedDeliveryPoint)
        {
            var shipmentUnloadFailedDomainEvent = new ShipmentUnloadFailedDomainEvent(this, failedDeliveryPoint);
            AddDomainEvent(shipmentUnloadFailedDomainEvent);
        }
    }
}
