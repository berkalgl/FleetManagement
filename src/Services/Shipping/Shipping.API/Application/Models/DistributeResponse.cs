namespace Shipping.API.Application.Models
{
    public record DistributeResponse
    {
        public string VehiclePlate { get; }
        public List<RouteResponse> Route { get; }
        public DistributeResponse(string vehiclePlate, List<RouteResponse> route)
        {
            VehiclePlate = vehiclePlate;
            Route = route;
        }

    }
    public record RouteResponse
    {
        public int DeliveryPoint { get; }
        public List<DeliveryResponse> Deliveries { get; }
        public RouteResponse(int deliveryPoint, List<DeliveryResponse> deliveries)
        {
            DeliveryPoint = deliveryPoint;
            Deliveries = deliveries;
        }
    }

    public record DeliveryResponse
    {
        public string Barcode { get; }
        public int State { get; }
        public DeliveryResponse(string barcode, int state)
        {
            Barcode = barcode;
            State = state;
        }
    }
}
