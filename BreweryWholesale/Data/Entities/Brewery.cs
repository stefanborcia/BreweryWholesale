using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Brewery
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual List<Beer> Beers { get; set; }
    }
}
