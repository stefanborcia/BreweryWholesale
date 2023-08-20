using System.ComponentModel.DataAnnotations;

namespace BreweryWholesale.Models
{
    public record QuotationDto : IValidatableObject
    {
        public IEnumerable<QuotationItemDto> QuotationItemDtos { get; set; } 

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var orderGroup = QuotationItemDtos
                .GroupBy(q => q.BeerId)
                .Where(o => o.Count() > 1);
            var duplicateOrder = orderGroup.Where(o => o.Count() > 1);

            if (duplicateOrder.Any())
            {
                return new[]
                {
                    new ValidationResult(
                        $"Duplicate items added by id {{{string.Join(',', duplicateOrder.Select(d => d.Key))}}}")
                };
            }
            return new ValidationResult[0];
        }
    }

    public record QuotationItemDto
    {
        public int BeerId { get; set; }
        public int Quantity { get; set; }
    }
}
