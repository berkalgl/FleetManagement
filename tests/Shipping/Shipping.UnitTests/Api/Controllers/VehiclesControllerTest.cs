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
            var vehiclesController = new VehiclesDistributionRequestController(_mediatorMock.Object);
            var actionResult = await vehiclesController.Distribute("", null);

            //Arrange
            Assert.IsType<BadRequestResult>(actionResult);
        }
        [Fact]
        public async Task Distribute_ValidRequest_ReturnsOk()
        {
            //Arrange
            var distributeCommand = GetFakeDistributeRequest();

            _mediatorMock
                .Setup(mediator => mediator.Send(distributeCommand, default))
                .ReturnsAsync(GetFakeDistributeResponse());

            //Act
            var vehiclesController = new VehiclesDistributionRequestController(_mediatorMock.Object);
            var actionResult = await vehiclesController.Distribute("34TL34", distributeCommand);

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

        private DistributeCommand GetFakeDistributeRequest()
        {
            return new DistributeCommand()
            {
                Routes = new List<Route>()
                {
                    new Route()
                    {
                        DeliveryPoint = 1,
                        Deliveries = new List<Delivery>()
                        {
                            new Delivery() { Barcode = "P7988000121"},
                            new Delivery() { Barcode = "P7988000122"},
                            new Delivery() { Barcode = "P7988000123"},
                            new Delivery() { Barcode = "P8988000121"},
                            new Delivery() { Barcode = "C725799"}
                        }
                    },
                    new Route()
                    {
                        DeliveryPoint = 2,
                        Deliveries= new List<Delivery>()
                        {
                            new Delivery() { Barcode = "P8988000123"},
                            new Delivery() { Barcode = "P8988000124"},
                            new Delivery() { Barcode = "P8988000125"},
                            new Delivery() { Barcode = "C725799"}
                        }
                    },
                    new Route()
                    {
                        DeliveryPoint = 3,
                        Deliveries= new List<Delivery>()
                        {
                            new Delivery() { Barcode = "P9988000126"},
                            new Delivery() { Barcode = "P9988000127"},
                            new Delivery() { Barcode = "P9988000128"},
                            new Delivery() { Barcode = "P9988000129"},
                            new Delivery() { Barcode = "P9988000130"}
                        }
                    }
                }
            };
        }
    }
}
