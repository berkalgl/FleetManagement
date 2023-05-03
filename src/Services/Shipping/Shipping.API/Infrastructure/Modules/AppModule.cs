using Shipping.Domain.AggregatesModel.ShipmentAggregate;
using Shipping.Infrastructure.Logging.Services;
using Shipping.Infrastructure.Repositories;

namespace Shipping.API.Infrastructure.Modules
{
    public static class AppModule
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IShipmentRepository, ShipmentRepository>();
            services.AddScoped<ILogService, DbLogService>();
        }
    }
}
