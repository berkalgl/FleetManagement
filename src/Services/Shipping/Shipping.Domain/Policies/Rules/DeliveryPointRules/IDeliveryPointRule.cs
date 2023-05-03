using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Policies.Rules.DeliveryPointRules
{
    public interface IDeliveryPointRule
    {
        bool CanUnloadShipment(Shipment shipment);
    }
}
