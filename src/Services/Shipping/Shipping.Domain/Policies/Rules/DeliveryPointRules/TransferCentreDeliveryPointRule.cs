using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Policies.Rules.DeliveryPointRules
{
    internal class TransferCentreDeliveryPointRule : IDeliveryPointRule
    {
        public bool CanUnloadShipment(Shipment shipment)
        {
            if (DeliveryPoint.TransferCentre != shipment.DeliveryPoint)
                return false;

            return shipment.GetShipmentDeliveryRule().CanBeDelivered(DeliveryPoint.TransferCentre);
        }
    }
}
