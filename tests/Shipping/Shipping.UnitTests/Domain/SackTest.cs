namespace Shipping.UnitTests.Domain
{
    public class SackTest
    {
        [Fact]
        public void Deliver_ExistsSack_AddsShipmentUnloadFailedDomainEvent()
        {
            //Arrange
            var package = new PackageBuilder("P9988000128", 3, 55).Build();
            var sack = new SackBuilder("C725799", 2)
                .AddPackage(package).Build();

            //Act
            sack.Deliver(1);

            //Assert
            Assert.Equal(1, sack.DomainEvents.Count);
        }
        [Fact]
        public void Deliver_ExistsSack_RemainsUnloadedStateAfterDelivery()
        {
            //Arrange
            var package = new PackageBuilder("P9988000128", 3, 55)
                .Build();
            var sack = new SackBuilder("C725799", 2)
                .AddPackage(package)
                .Build();

            //Act
            sack.Deliver(2);

            //Assert
            Assert.Equal(4, sack.SackStateId);
        }
    }
}
