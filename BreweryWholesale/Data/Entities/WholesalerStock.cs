namespace BreweryWholesale.Data.Entities
{
    public class WholesalerStock
    {
        public int SaleId { get; set; }
        public int WholesalerId { get; set; }
        public int BeerId { get; set; }
        public int Stock { get; set; }
    }
}
