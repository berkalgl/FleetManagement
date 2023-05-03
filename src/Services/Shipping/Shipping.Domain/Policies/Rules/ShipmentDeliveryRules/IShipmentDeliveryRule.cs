using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Policies.Rules.ShipmentDeliveryRules
{
    public interface IShipmentDeliveryRule
    {
        bool CanBeDelivered(DeliveryPoint deliveryPoint);
    }
}
