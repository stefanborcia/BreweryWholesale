using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Beer
    {
        [Key]
        public int Id { get; set; }
        public int BrewerId { get; set; }
        public string? Name { get; set; }
        public string? Alcohol { get; set; }
        public double? Price { get; set; }
        public virtual Brewery Brewery { get; set; }

    }
}
