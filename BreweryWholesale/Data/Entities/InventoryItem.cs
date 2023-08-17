namespace BreweryWholesale.Data.Entities
{
    public class InventoryItem
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }

        public int BeerId { get; set; }
        public Beer Beer { get; set; }
        public Inventory Inventory { get; set; }
    }
}
