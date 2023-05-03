using Microsoft.EntityFrameworkCore;
using Shipping.Infrastructure;
using System;
using TechTalk.SpecFlow;

namespace Shipping.UnitTests
{
    [Binding]
    public class ShipmentFeatureStepDefinitions
    {
        private readonly ShipmentDbContext _dbContext;
        public ShipmentFeatureStepDefinitions()
        {
            var options = new DbContextOptionsBuilder<ShipmentDbContext>()
                .UseInMemoryDatabase(databaseName: "ShipmentTestDb")
                .Options;

            _dbContext = new ShipmentDbContext(options);
        }
        [Given(@"'([^']*)' package in the database and created")]
        public async void GivenPackageİnTheDatabaseAndCreated(string p0)
        {
            var package = new PackageBuilder("P8988000120", 2, 33).Build();
            await _dbContext.Packages.AddAsync(package);
            await _dbContext.SaveChangesAsync();
        }

        [When(@"I tried to deliver other packages and not '([^']*)' package")]
        public async void WhenITriedToDeliverOtherPackagesAndNotPackage(string p0)
        {
            var package = new PackageBuilder("P8988000123", 2, 33).Build();
            await _dbContext.Packages.AddAsync(package);
            await _dbContext.SaveChangesAsync();

            package.Deliver(2);
            await _dbContext.SaveChangesAsync();
        }

        [Then(@"The state of the package '([^']*)' should be Created")]
        public async void ThenTheStateOfThePackageShouldBeCreated(string p0)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Barcode.Equals("P8988000120"));

            Assert.Equal(1, package?.PackageStateId);
        }

        [Given(@"'([^']*)' package in the database")]
        public async void GivenPackageİnTheDatabase(string p0)
        {
            var package = new PackageBuilder("P8988000121", 2, 17).Build();
            await _dbContext.Packages.AddAsync(package);
            await _dbContext.SaveChangesAsync();
        }

        [When(@"I tried to deliver '([^']*)'")]
        public async void WhenITriedToDeliver(string p0)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Barcode.Equals("P8988000121"));

            package?.Deliver(1);
            await _dbContext.SaveChangesAsync();
        }

        [Then(@"The state of the package with the barcode '([^']*)' should be Loaded")]
        public async void ThenTheStateOfThePackageWithTheBarcodeShouldBeLoaded(string p0)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Barcode.Equals("P8988000121"));

            Assert.Equal(3, package?.PackageStateId);
        }

        [Given(@"'([^']*)' sack in the database")]
        public async void GivenSackİnTheDatabase(string p0)
        {
            var sack = new SackBuilder("C725800", 3).Build();
            await _dbContext.Sacks.AddAsync(sack);
            await _dbContext.SaveChangesAsync();
        }

        [When(@"I tried to deliver the sack'([^']*)'")]
        public async void WhenITriedToDeliverTheSack(string p0)
        {
            var sack = await _dbContext.Sacks.FirstOrDefaultAsync(p => p.Barcode.Equals("C725800"));

            sack?.Deliver(3);
            await _dbContext.SaveChangesAsync();
        }

        [Then(@"The state of the sack with the barcode '([^']*)' should be unloaded")]
        public async void ThenTheStateOfTheSackWithTheBarcodeShouldBeUnloaded(string p0)
        {
            var sack = await _dbContext.Sacks.FirstOrDefaultAsync(p => p.Barcode.Equals("C725800"));
            Assert.Equal(4, sack?.SackStateId);
        }

        [Given(@"'([^']*)' package in the database for unloaded")]
        public async void GivenPackageİnTheDatabaseForUnloaded(string p0)
        {
            var package = new PackageBuilder("P8988000122", 2, 17).Build();
            await _dbContext.Packages.AddAsync(package);
            await _dbContext.SaveChangesAsync();
        }

        [When(@"I tried to deliver package '([^']*)'")]
        public async void WhenITriedToDeliverPackage(string p0)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Barcode.Equals("P8988000122"));

            package?.Deliver(1);
            await _dbContext.SaveChangesAsync();
        }

        [Then(@"The state of the package with the barcode '([^']*)' should be unloaded")]
        public async void ThenTheStateOfThePackageWithTheBarcodeShouldBeUnloaded(string p0)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Barcode.Equals("P8988000122"));

            Assert.Equal(3, package?.PackageStateId);
        }

        [Given(@"'([^']*)' pack in the database for unloaded")]
        public async void GivenPackİnTheDatabaseForUnloaded(string p0)
        {
            var package = new PackageBuilder("P8988000126", 2, 17).Build();
            await _dbContext.Packages.AddAsync(package);
            await _dbContext.SaveChangesAsync();
        }

        [When(@"I tried to deliver the pack '([^']*)'")]
        public async void WhenITriedToDeliverThePack(string p0)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Barcode.Equals("P8988000126"));

            package?.Deliver(1);
            await _dbContext.SaveChangesAsync();
        }

        [Then(@"The state of the pack with the barcode '([^']*)' should be unloaded")]
        public async void ThenTheStateOfThePackWithTheBarcodeShouldBeUnloaded(string p0)
        {
            var package = await _dbContext.Packages.FirstOrDefaultAsync(p => p.Barcode.Equals("P8988000126"));

            Assert.Equal(3, package?.PackageStateId);
        }
    }
}
