using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Brewery
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public virtual List<Beer> Beers { get; set; }
        public Brewery()
        {
            Beers = new List<Beer>();
        }
    }
}
