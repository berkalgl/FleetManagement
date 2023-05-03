using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.UnitTests
{
    public class SackBuilder
    {
        private readonly Sack sack;

        public SackBuilder(string barcode, int deliveryPointId)
        {
            sack = new Sack(barcode, deliveryPointId);
        }
        public SackBuilder AddPackage(Package? package)
        {
            sack.AddPackage(package);
            return this;
        }
        public Sack Build()
        {
            return sack;
        }
    }
}
