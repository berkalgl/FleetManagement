using Microsoft.EntityFrameworkCore;
using Shipping.Domain.AggregatesModel.ShipmentAggregate;
using Shipping.Domain.SeedWork;
using MediatR;

namespace Shipping.Infrastructure
{
    public class ShipmentDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "ShipmentDbSchema";
        public virtual DbSet<Shipment> Shipments { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageState> PackageStates { get; set; }
        public virtual DbSet<Sack> Sacks { get; set; }
        public virtual DbSet<SackState> SackStates { get; set; }
        public virtual DbSet<DeliveryPoint> DeliveryPoints { get; set; }
        private readonly IMediator _mediator;
        public ShipmentDbContext() { }
        public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options) : base(options) { }
        public ShipmentDbContext(DbContextOptions<ShipmentDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipment>()
                .HasDiscriminator<string>("shipment_type")
                .HasValue<Package>("package")
                .HasValue<Sack>("sack");
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
