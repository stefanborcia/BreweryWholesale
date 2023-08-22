using BreweryWholesale.Data.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace BreweryWholesale.Models
{
    public record BeerDto
    {
        [SwaggerParameter("Beer Id")]
        public int Id { get; set; }
        [SwaggerParameter("Beer Name")]
        public string Name { get; set; }
        [SwaggerParameter("Alcohol Content (%)")]
        public decimal AlcoholContent { get; set; }
        [SwaggerParameter("Beer Price")]
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
