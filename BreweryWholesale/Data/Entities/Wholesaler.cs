using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Data.Entities
{
    public class Wholesaler
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = null!;
        public ICollection<Inventory>? InventoryItems { get; set; }

        public Wholesaler()
        {
            InventoryItems = new List<Inventory>();
        }
    }
}
