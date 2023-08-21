using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Inventory
    {
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string? BeerName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Wholesaler? Wholesaler { get; set; }
    }
}
