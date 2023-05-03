using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Policies.Rules.ShipmentDeliveryRules
{
    public class SackDeliveryRule : IShipmentDeliveryRule
    {
        private readonly Sack _sack;
        public SackDeliveryRule(Sack sack)
        {
            _sack = sack;
        }
        public bool CanBeDelivered(DeliveryPoint deliveryPoint)
        {
            return deliveryPoint != DeliveryPoint.Branch;
        }
    }
}
