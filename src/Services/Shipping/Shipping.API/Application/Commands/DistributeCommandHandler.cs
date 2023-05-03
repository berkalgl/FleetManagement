using MediatR;
using Shipping.API.Application.Models;
using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.API.Application.Commands
{
    public class DistributeCommandHandler : IRequestHandler<DistributeCommand, DistributeResponse>
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly ILogger<DistributeCommandHandler> _logger;
        public DistributeCommandHandler(IShipmentRepository shipmentRepository, ILogger<DistributeCommandHandler> logger)
        {
            _shipmentRepository = shipmentRepository;
            _logger = logger;
        }
        public async Task<DistributeResponse> Handle(DistributeCommand request, CancellationToken cancellationToken)
        {
            List<RouteResponse> routes = new List<RouteResponse>();

            //O(m*n)
            request.Routes.ForEach(r => 
            {
                List<DeliveryResponse> deliveries = new List<DeliveryResponse>();

                r.Deliveries.ForEach(async d =>
                {
                    var shipment = await _shipmentRepository.GetBy(d.Barcode);

                    if (shipment is null)
                    {
                        _logger.LogWarning($"Could not find the shipment with the barcode {d.Barcode}");
                        return; 
                    } 

                    shipment.Deliver(r.DeliveryPoint);
                    deliveries.Add(new(shipment.Barcode, shipment is Package ? ((Package)shipment).PackageStateId : ((Sack)shipment).SackStateId));
                });

                routes.Add(new RouteResponse(r.DeliveryPoint, deliveries));
            });

            await _shipmentRepository.UnitOfWork.SaveEntitiesAsync();
            return new DistributeResponse(request.VehiclePlate, routes);
        }
    }
}
