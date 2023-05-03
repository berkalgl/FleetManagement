using Shipping.Domain.AggregatesModel.ShipmentAggregate;
using Shipping.Domain.Exceptions;

namespace Shipping.UnitTests.Domain
{
    public class PackageTest
    {
        [Fact]
        public void Construct_ExistsBarcode_RemainsCreatedStatus()
        {
            //Arrange
            var package = new PackageBuilder("P8988000120", 2, 33).Build();

            //Act

            //Assert
            Assert.Equal(1, package.PackageStateId);
        }

        [Fact]
        public void Deliver_ExistsPackage_AddsShipmentUnloadFailedDomainEvent()
        {
            //Arrange
            var package = new PackageBuilder("P8988000121", 2, 17).Build();

            //Act
            package.Deliver(1);

            //Assert
            Assert.Equal(1, package.DomainEvents.Count);
        }
        [Fact]
        public void Deliver_ExistsPackage_RemainsLoadedStateAfterDelivery()
        {
            //Arrange
            var package = new PackageBuilder("P8988000121", 2, 17).Build();

            //Act
            package.Deliver(1);

            //Assert
            Assert.Equal(3, package.PackageStateId);
        }
        [Fact]
        public void Deliver_ExistsPackage_RemainsUnloadedStateAfterDelivery_ToBranch()
        {
            //Arrange
            var package = new PackageBuilder("P7988000121", 1, 5)
                .Build();

            //Act
            package.Deliver(1);

            //Assert
            Assert.Equal(4, package.PackageStateId);
        }
        [Fact]
        public void Deliver_ExistsPackageInSack_RemainsUnloadedStateAfterDelivery_ToDistributionCenter()
        {
            //Arrange
            var package = new PackageBuilder("P8988000122", 2, 26)
                .SetSack(new Sack("C725799", 2))
                .Build();

            //Act
            package.Deliver(2);

            //Assert
            Assert.Equal(4, package.PackageStateId);
        }
        [Fact]
        public void Deliver_ExistsPackageInSack_RemainsUnloadedStateAfterDelivery_ToTransferCenter()
        {
            //Arrange
            var package = new PackageBuilder("P9988000128", 3, 55)
                .SetSack(new Sack("C725800", 3))
                .Build();

            //Act
            package.Deliver(3);

            //Assert
            Assert.Equal(4, package.PackageStateId);
        }
        [Fact]
        public void Deliver_ExistsPackage_ThrowsException_WithInvalidDeliveryPoint()
        {
            //Arrange
            var package = new PackageBuilder("P7988000121", 3, 55)
                .Build();

            //Act

            //Assert
            Assert.Throws<ShippingDomainException>(() => package.Deliver(4));
        }
        [Fact]
        public void Deliver_ExistsPackage_RemainsLoadedStateAfterDelivery_ToTransferCenter_WithWrongDeliveryPoint()
        {
            //Arrange
            var package = new PackageBuilder("P7988000121", 1, 55)
                .Build();

            //Act
            package.Deliver(3);

            //Assert
            Assert.Equal(3, package.PackageStateId);
        }
    }
}
