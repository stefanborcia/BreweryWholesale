using BreweryWholesale.Data.Entities;

namespace BreweryWholesale.Models
{
    public record WholesalerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static IEnumerable<WholesalerDto> WholesalerDtos(IEnumerable<Wholesaler> wholesalers)
        {
            return wholesalers.Select(w =>
            {
                return new WholesalerDto
                {
                    Id = w.Id,
                    Name = w.Name,
                };
            });
        }
    }
}
