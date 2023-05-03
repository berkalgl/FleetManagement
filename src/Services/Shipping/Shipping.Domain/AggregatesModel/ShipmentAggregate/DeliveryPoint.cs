using Shipping.Domain.Exceptions;
using Shipping.Domain.Policies.Rules.DeliveryPointRules;
using Shipping.Domain.SeedWork;

namespace Shipping.Domain.AggregatesModel.ShipmentAggregate
{
    public class DeliveryPoint : Enumeration
    {
        public static DeliveryPoint Branch = new(1, nameof(Branch));
        public static DeliveryPoint DistributionCentre = new(2, nameof(DistributionCentre));
        public static DeliveryPoint TransferCentre = new(3, nameof(TransferCentre));
        public DeliveryPoint(int id, string name)
        : base(id, name)
        {
        }
        public static IEnumerable<DeliveryPoint> List() =>
            new[] { Branch, DistributionCentre, TransferCentre };
        public static DeliveryPoint From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);
            if (state == null)
                throw new ShippingDomainException($"Possible values for DeliveryPoint: {string.Join(",", List().Select(s => s.Name))}");

            return state;
        }
        public IDeliveryPointRule GetDeliveryPointRule()
        {
            IDeliveryPointRule DeliveryPointRule;
            DeliveryPointRule = Name switch
            {
                "Branch" => new BranchDeliveryPointRule(),
                "DistributionCentre" => new DistributionCenterDeliveryPointRule(),
                "TransferCentre" => new TransferCentreDeliveryPointRule(),
                _ => throw new ShippingDomainException("Invalid DeliveryPoint for getting the rule"),
            };
            return DeliveryPointRule;
        }
    }
}
