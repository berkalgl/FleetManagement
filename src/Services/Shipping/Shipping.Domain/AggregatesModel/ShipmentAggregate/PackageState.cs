using Shipping.Domain.Exceptions;
using Shipping.Domain.SeedWork;

namespace Shipping.Domain.AggregatesModel.ShipmentAggregate
{
    public class PackageState : Enumeration
    {
        public static PackageState Created = new(1, nameof(Created));
        public static PackageState LoadedIntoSack = new(2, nameof(LoadedIntoSack));
        public static PackageState Loaded = new(3, nameof(Loaded));
        public static PackageState Unloaded = new(4, nameof(Unloaded));

        public PackageState(int id, string name)
        : base(id, name)
        {
        }
        public static IEnumerable<PackageState> List() =>
            new[] { Created, LoadedIntoSack, Loaded, Unloaded };
        public static PackageState From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ShippingDomainException($"Possible values for PackageState: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
        public static IEnumerable<PackageState> LoadedStateList() =>
            new[] { LoadedIntoSack, Loaded };
    }
}
