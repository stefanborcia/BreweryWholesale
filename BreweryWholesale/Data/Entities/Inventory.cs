namespace BreweryWholesale.Data.Entities
{
    public class Inventory
    {
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Stock { get; set; }
        public string BeerName { get; set; }
        public double Price { get; set; }
        public Wholesaler Wholesaler { get; set; }
    }
}
