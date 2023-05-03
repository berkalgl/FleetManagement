using Shipping.Domain.Policies.Rules.ShipmentDeliveryRules;

namespace Shipping.Domain.AggregatesModel.ShipmentAggregate
{
    public class Sack : Shipment
    {
        public int SackStateId { get; private set; }
        public ICollection<Package> Packages { get; private set; }
        public Sack(string barcode, int deliveryPointId) : base(barcode, deliveryPointId)
        {
            SackStateId = SackState.Created.Id;
            Packages = new List<Package>();
        }
        public Sack AddPackage(Package? package)
        {
            Packages.Add(package);
            return this;
        }
        public override Shipment Load()
        {
            SackStateId = SackState.Loaded.Id;
            return this;
        }
        public override Shipment Unload(int deliveryPoint)
        {
            if (CanUnload(deliveryPoint))
                SackStateId = SackState.Unloaded.Id;

            return this;
        }

        public override Shipment Deliver(int deliveryPoint)
        {
            Packages.ToList().ForEach(package => { package.LoadIntoSack(); });

            Load();

            Packages.ToList().ForEach(package => { package.Unload(deliveryPoint); });
            Unload(deliveryPoint);

            return this;

        }

        public override IShipmentDeliveryRule GetShipmentDeliveryRule()
        {
            return new SackDeliveryRule(this);
        }
    }
}
