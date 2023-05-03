using Shipping.Domain.SeedWork;

namespace Shipping.Domain.AggregatesModel.ShipmentAggregate
{
    public interface IShipmentRepository : IRepository<Shipment>
    {
        Task<Shipment?> GetBy(string barcode);
    }
}
