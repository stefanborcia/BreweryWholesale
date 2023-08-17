namespace BreweryWholesale.Data.Entities
{
    public class Wholesaler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Inventory> InventoryItems { get; set; }
    }
}
