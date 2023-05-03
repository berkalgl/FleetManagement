using Shipping.Domain.Policies.Rules.ShipmentDeliveryRules;

namespace Shipping.Domain.AggregatesModel.ShipmentAggregate
{
    public class Package : Shipment
    {
        public int PackageStateId { get; private set; }
        public PackageState PackageState => PackageState.From(PackageStateId);
        public int Desi { get; private set; }
        public Sack? Sack { get; private set; }
        public Package(string barcode, int deliveryPointId, int desi) : base(barcode, deliveryPointId)
        {
            Desi = desi;
            PackageStateId = PackageState.Created.Id;
        }
        public Package SetSack(Sack? sack)
        {
            Sack = sack;
            return this;
        }
        public override Shipment Load()
        {
            if (!PackageState.LoadedStateList().Contains(PackageState))
                PackageStateId = PackageState.Loaded.Id;

            return this;
        }
        public Package LoadIntoSack()
        {
            if (!PackageState.LoadedStateList().Contains(PackageState))
                PackageStateId = PackageState.LoadedIntoSack.Id;
            return this;
        }
        public override Shipment Unload(int deliveryPoint)
        {
            if (CanUnload(deliveryPoint))
                PackageStateId = PackageState.Unloaded.Id;

            return this;
        }
        public override Shipment Deliver(int deliveryPoint)
        {
            if(Sack is not null)
            {
                LoadIntoSack();
                Unload(deliveryPoint);
                return this;
            }

            Load();
            Unload(deliveryPoint);

            return this;
        }

        public override IShipmentDeliveryRule GetShipmentDeliveryRule()
        {
            return new PackageDeliveryRule(this);
        }
    }
}
