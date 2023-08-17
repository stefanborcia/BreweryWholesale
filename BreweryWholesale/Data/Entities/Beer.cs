using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Beer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double AlcoholContent { get; set; }
        public double Price { get;set; }
        public int BreweryId { get;set; }
        public Brewery Brewery { get; set; }
    }
}
