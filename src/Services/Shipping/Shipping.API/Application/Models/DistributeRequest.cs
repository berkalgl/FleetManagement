namespace Shipping.API.Application.Models
{
    public record DistributeRequest
    {
        public List<RouteRequest> Route { get; set; }
    }
    public record RouteRequest
    {
        public int DeliveryPoint { get; set; }
        public List<DeliveryRequest> Deliveries { get; set; }
    }

    public record DeliveryRequest
    {
        public string Barcode { get; set; }
    }
}
