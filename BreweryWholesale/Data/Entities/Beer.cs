using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlcoholContent { get; set; }
        public decimal Price { get;set; }
    }
}
