using MediatR;
using Shipping.API.Application.Models;

namespace Shipping.API.Application.Commands
{
    public class DistributeCommand : IRequest<DistributeResponse>
    {
        public string VehiclePlate { get; init; }
        public List<Route> Routes { get; init; }

    }
    public class Route
    {
        public int DeliveryPoint { get; init; }
        public List<Delivery> Deliveries { get; init; }
    }
    public class Delivery
    {
        public string Barcode { get; init; }
    }
}
