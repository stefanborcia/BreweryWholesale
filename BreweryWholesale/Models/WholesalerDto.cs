using BreweryWholesale.Data.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace BreweryWholesale.Models
{
    public record WholesalerDto
    {
        [SwaggerParameter("Wholesaler Id")]
        public int Id { get; set; }
        [SwaggerParameter("Wholesaler Name")]
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
