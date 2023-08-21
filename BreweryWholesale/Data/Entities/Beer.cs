using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Beer
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public decimal AlcoholContent { get; set; }
        [Required]
        public decimal Price { get;set; }
        public Brewery? Brewery { get; set; }
    }
}
