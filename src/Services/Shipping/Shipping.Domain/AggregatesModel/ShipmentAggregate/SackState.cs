using Shipping.Domain.SeedWork;

namespace Shipping.Domain.AggregatesModel.ShipmentAggregate
{
    public class SackState : Enumeration
    {
        public static SackState Created = new(1, nameof(Created));
        public static SackState Loaded = new(3, nameof(Loaded));
        public static SackState Unloaded = new(4, nameof(Unloaded));

        public SackState(int id, string name)
        : base(id, name)
        {
        }
    }
}
