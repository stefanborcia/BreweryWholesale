using BreweryWholesale.Data.Entities;

namespace BreweryWholesale.Models
{
    public record BeerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal AlcoholContent { get; set; }
        public decimal Price { get; set; }

        public static IEnumerable<BeerDto> BeerDtos(IEnumerable<Beer> beers)
        {
            return beers.Select(b =>
            {
                return new BeerDto()
                {
                    Id = b.Id,
                    Name = b.Name,
                    AlcoholContent = b.AlcoholContent,
                    Price = b.Price
                };
            });
        }
        internal Beer ToBeer(Brewery brewery) 
        {
            return new Beer()
            {
                Name = Name,
                AlcoholContent = AlcoholContent,
                Price = Price,
                Brewery = brewery
            };
        }
        internal static BeerDto From(Beer beer)
        {
            return new BeerDto()
            {
                Id = beer.Id,
                Name = beer.Name,
                AlcoholContent = beer.AlcoholContent,
                Price = beer.Price
            };
        }
    }
}
