using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Policies.Rules.DeliveryPointRules
{
    public class BranchDeliveryPointRule : IDeliveryPointRule
    {
        public bool CanUnloadShipment(Shipment shipment)
        {
            if (DeliveryPoint.Branch != shipment.DeliveryPoint)
                return false;

            return shipment.GetShipmentDeliveryRule().CanBeDelivered(DeliveryPoint.Branch);
        }
    }
}
