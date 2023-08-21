namespace BreweryWholesale.Models
{
    public record WholesalerBuyDto
    {
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
    public record OrderItemDto
    {
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
}
