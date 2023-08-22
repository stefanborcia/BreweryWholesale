using Swashbuckle.AspNetCore.Annotations;

namespace BreweryWholesale.Models
{
    public record WholesalerBuyDto
    {
        [SwaggerParameter("Wholesaler Order Items")]
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
    public record OrderItemDto
    {
        [SwaggerParameter("Beer Id")]
        public int BeerId { get; set; }
        [SwaggerParameter("Item Quantity")]
        public int Quantity { get; set; }
    }
}
