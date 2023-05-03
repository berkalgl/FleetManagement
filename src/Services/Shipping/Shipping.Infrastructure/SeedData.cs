using Shipping.Domain.AggregatesModel.ShipmentAggregate;

namespace Shipping.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(ShipmentDbContext shipmentDbContext)
        {
            Seed(shipmentDbContext);
        }
        private static void Seed(ShipmentDbContext shipmentDbContext)
        {
            if (!shipmentDbContext.Packages.Any())
            {
                var packages = new List<Package>
                {
                    new Package("P7988000121", 1, 5),
                    new Package("P7988000122", 1, 5),
                    new Package("P7988000123", 1, 9),
                    new Package("P8988000120", 2, 33),
                    new Package("P8988000121", 2, 17),
                    new Package("P8988000122", 2, 26),
                    new Package("P8988000123", 2, 35),
                    new Package("P8988000124", 2, 1),
                    new Package("P8988000125", 2, 200),
                    new Package("P8988000126", 2, 50),
                    new Package("P9988000126", 3, 15),
                    new Package("P9988000127", 3, 16),
                    new Package("P9988000128", 3, 55),
                    new Package("P9988000129", 3, 28),
                    new Package("P9988000130", 3, 17)
                };

                shipmentDbContext.Packages.AddRange(packages);
                shipmentDbContext.SaveChanges();

                if (!shipmentDbContext.Sacks.Any())
                {
                    var sacks = new List<Sack>
                    {
                        new Sack("C725799", 2)
                        .AddPackage(packages.FirstOrDefault(p => p.Barcode.Equals("P8988000122")))
                        .AddPackage(packages.FirstOrDefault(p => p.Barcode.Equals("P8988000126"))),
                        new Sack("C725800", 3)
                        .AddPackage(packages.FirstOrDefault(p => p.Barcode.Equals("P9988000128")))
                        .AddPackage(packages.FirstOrDefault(p => p.Barcode.Equals("P9988000129")))
                    };

                    shipmentDbContext.Sacks.AddRange(sacks);
                    shipmentDbContext.SaveChanges();

                }

            }

        }
    }
}
