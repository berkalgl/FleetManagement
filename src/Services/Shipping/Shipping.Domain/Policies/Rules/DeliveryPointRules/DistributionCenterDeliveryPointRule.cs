using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Policies.Rules.DeliveryPointRules
{
    public class DistributionCenterDeliveryPointRule : IDeliveryPointRule
    {

        public bool CanUnloadShipment(Shipment shipment)
        {
            if (DeliveryPoint.DistributionCentre != shipment.DeliveryPoint)
                return false;

            return shipment.GetShipmentDeliveryRule().CanBeDelivered(DeliveryPoint.DistributionCentre);
        }
    }
}
