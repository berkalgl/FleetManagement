using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Domain.Policies.Rules.ShipmentDeliveryRules
{
    public class PackageDeliveryRule : IShipmentDeliveryRule
    {
        private readonly Package Package;
        public PackageDeliveryRule(Package package)
        {
            Package = package;
        }
        public bool CanBeDelivered(DeliveryPoint deliveryPoint)
        {
            if (deliveryPoint == DeliveryPoint.DistributionCentre)
                return Package.PackageState == PackageState.Loaded || Package.PackageState == PackageState.LoadedIntoSack;

            if (deliveryPoint == DeliveryPoint.TransferCentre)
                return Package.PackageState == PackageState.LoadedIntoSack;

            //Branch
            return Package.PackageState == PackageState.Loaded;

        }
    }
}
