using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.UnitTests
{
    public class PackageBuilder
    {
        private readonly Package package;

        public PackageBuilder(string barcode, int deliveryPointId, int desi)
        {
            package = new Package(barcode, deliveryPointId, desi);
        }
        public PackageBuilder SetSack(Sack? sack)
        {
            package.SetSack(sack);
            return this;
        }
        public Package Build()
        {
            return package;
        }
    }
}
