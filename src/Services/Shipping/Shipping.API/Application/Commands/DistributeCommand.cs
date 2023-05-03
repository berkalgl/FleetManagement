using MediatR;
using Shipping.API.Application.Models;

namespace Shipping.API.Application.Commands
{
    public class DistributeCommand : IRequest<DistributeResponse>
    {
        public string VehiclePlate { get; }
        public List<Route> Routes { get; }

        public DistributeCommand(string vehiclePlate, List<Route> routes)
        {
            VehiclePlate = vehiclePlate;
            Routes = routes;
        }

        public static DistributeCommand FromRequest(string vehiclePlate, DistributeRequest distributeRequest)
        {
            return new DistributeCommand(vehiclePlate, distributeRequest.Route.Select(Route.FromRequest).ToList());
        }

    }
    public class Route
    {
        public int DeliveryPoint { get; }
        public List<Delivery> Deliveries { get; }
        public Route(int deliveryPoint, List<Delivery> deliveries)
        {
            DeliveryPoint = deliveryPoint;
            Deliveries = deliveries;
        }
        public static Route FromRequest(RouteRequest routeRequest)
        {
            return new Route(routeRequest.DeliveryPoint, routeRequest.Deliveries.Select(Delivery.FromRequest).ToList());
        }
    }
    public class Delivery
    {
        public string Barcode { get; }
        public Delivery(string barcode)
        {
            Barcode = barcode;
        }
        public static Delivery FromRequest(DeliveryRequest deliveryRequest)
        {
            return new Delivery(deliveryRequest.Barcode);
        }
    }
}
