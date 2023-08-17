using BreweryWholesale.Data.Entities;

namespace BreweryWholesale.Models
{
    public record BeerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AlcoholContent { get; set; }
        public double Price { get; set; }

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
    }
}
