using Microsoft.EntityFrameworkCore;
using Shipping.Domain.AggregatesModel.ShipmentAggregate;
using Shipping.Domain.SeedWork;

namespace Shipping.Infrastructure.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly ShipmentDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;
        public ShipmentRepository(ShipmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shipment?> GetBy(string barcode)
        {
            return await _dbContext.Shipments
                .Where(s => s.Barcode.Equals(barcode))
                .Include(s => ((Sack)s).Packages)
                .Include(s => ((Package)s).Sack)
                .FirstOrDefaultAsync();
        }
    }
}
