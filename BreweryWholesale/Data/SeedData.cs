using BreweryWholesale.Data.Entities;

namespace BreweryWholesale.Data
{
    public class SeedData
    {
        public static void AddData(BreweryContext context)
        {
            if (!context.Brewerys.Any())
            {
                var brewerys = GetBrewerys().ToList();
                brewerys.ForEach(b=>b.Beers.Add(GetBeer(b)));
                context.Brewerys.AddRange(brewerys);
            }

            if (!context.Wholesalers.Any())
            {
                var wholesalers = GetWholesalers().ToList();
                wholesalers.ForEach(b=>b.InventoryItems.Add(GetInventory(b)));
                context.Wholesalers.AddRange(wholesalers);
            }
        }

        private static IEnumerable<Brewery> GetBrewerys()
        {
            return new List<Brewery>
            {
                new Brewery()
                {
                    Name = "Abbaye de Leffe"
                }
            };
        }

        private static Beer GetBeer(Brewery brewery)
        {
            return new Beer
            {
                Brewery = brewery,
                Name = "Leffe Blonde",
                AlcoholContent = 6.6m,
                Price = 2.20m
            };
        }

        private static IEnumerable<Wholesaler> GetWholesalers()
        {
            return new List<Wholesaler>
            {
                new Wholesaler()
                {
                    Name = "GeneDrinks"
                }
            };
        }

        private static Inventory GetInventory(Wholesaler wholesaler)
        {
            return new Inventory
            {
                BeerId = 1,
                Stock = 10,
                BeerName = "Leffe Blonde",
                Price = 2.2m,
                Wholesaler = wholesaler
            };
        }
    }
}
