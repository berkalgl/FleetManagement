using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shipping.API.Application.Commands;
using Shipping.API.Application.Models;
using Shipping.API.Controllers;
using Shipping.Domain.Exceptions;

namespace Shipping.UnitTests.Api.Controllers
{
    public class VehiclesControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public VehiclesControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }
        [Fact]
        public async Task Distribute_NullRequest_ReturnsBadRequest()
        {
            //Arrange

            //Act
            var vehiclesController = new VehiclesController(_mediatorMock.Object);
            var actionResult = await vehiclesController.Distribute("", null);

            //Arrange
            Assert.IsType<BadRequestResult>(actionResult);
        }
        [Fact]
        public async Task Distribute_ValidRequest_ReturnsOk()
        {
            //Arrange
            var distributeRequest = GetFakeDistributeRequest();
            var distributeCommand = DistributeCommand.FromRequest("34TL34", distributeRequest);

            _mediatorMock
                .Setup(mediator => mediator.Send(distributeCommand, default))
                .ReturnsAsync(GetFakeDistributeResponse());

            //Act
            var vehiclesController = new VehiclesController(_mediatorMock.Object);
            var actionResult = await vehiclesController.Distribute("34TL34", distributeRequest);

            //Arrange
            Assert.IsType<OkObjectResult>(actionResult);
        }
        private DistributeResponse GetFakeDistributeResponse()
        {
            return new DistributeResponse(
                "34TL34", 
                new List<RouteResponse>()
                {
                    new RouteResponse(1, new List<DeliveryResponse>()
                        {
                            new DeliveryResponse("P7988000121", 4),
                            new DeliveryResponse("P7988000122", 4),
                            new DeliveryResponse("P7988000123", 4),
                            new DeliveryResponse("P8988000121", 3),
                            new DeliveryResponse("C725799", 3),
                        }),
                    new RouteResponse(2, new List<DeliveryResponse>()
                        {
                            new DeliveryResponse("P8988000123", 4),
                            new DeliveryResponse("P8988000124", 4),
                            new DeliveryResponse("P8988000125", 4),
                            new DeliveryResponse("C725799", 4)
                        }),
                    new RouteResponse(2, new List<DeliveryResponse>()
                        {
                            new DeliveryResponse("P9988000126", 3),
                            new DeliveryResponse("P9988000127", 3),
                            new DeliveryResponse("P9988000128", 4),
                            new DeliveryResponse("P9988000129", 4),
                            new DeliveryResponse("P9988000130", 3),
                        }),
                });
        }

        private DistributeRequest GetFakeDistributeRequest()
        {
            return new DistributeRequest()
            {
                Route = new List<RouteRequest>()
                {
                    new RouteRequest()
                    {
                        DeliveryPoint = 1,
                        Deliveries = new List<DeliveryRequest>()
                        {
                            new DeliveryRequest() { Barcode = "P7988000121"},
                            new DeliveryRequest() { Barcode = "P7988000122"},
                            new DeliveryRequest() { Barcode = "P7988000123"},
                            new DeliveryRequest() { Barcode = "P8988000121"},
                            new DeliveryRequest() { Barcode = "C725799"}
                        }
                    },
                    new RouteRequest()
                    {
                        DeliveryPoint = 2,
                        Deliveries= new List<DeliveryRequest>()
                        {
                            new DeliveryRequest() { Barcode = "P8988000123"},
                            new DeliveryRequest() { Barcode = "P8988000124"},
                            new DeliveryRequest() { Barcode = "P8988000125"},
                            new DeliveryRequest() { Barcode = "C725799"}
                        }
                    },
                    new RouteRequest()
                    {
                        DeliveryPoint = 3,
                        Deliveries= new List<DeliveryRequest>()
                        {
                            new DeliveryRequest() { Barcode = "P9988000126"},
                            new DeliveryRequest() { Barcode = "P9988000127"},
                            new DeliveryRequest() { Barcode = "P9988000128"},
                            new DeliveryRequest() { Barcode = "P9988000129"},
                            new DeliveryRequest() { Barcode = "P9988000130"}
                        }
                    }
                }
            };
        }
    }
}
