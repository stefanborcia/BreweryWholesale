using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using BreweryWholesale.Data;
using BreweryWholesale.Data.Entities;
using BreweryWholesale.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Controllers
{
    [Route("api/wholesaler")]
    [ApiController]
    public class WholesalerController : ControllerBase
    {
        private readonly BreweryContext _context;
        public WholesalerController(BreweryContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> AddWholesaler([FromBody, Required] string name)
        {
            var wholesaler = new Wholesaler() { Name = name };
             await _context.Wholesalers.AddAsync(wholesaler);
             await _context.SaveChangesAsync();
             return Created($"/{wholesaler.Id}",null);
        }
        [HttpGet]
        [Route("{wholesalerId:int}")]
        public async Task<ActionResult<Wholesaler>> GetWholesaler(int wholesalerId)
        {
            var wholesaler = await _context
                .Wholesalers
                .Include(w=>w.InventoryItems)
                .FirstOrDefaultAsync(w => w.Id == wholesalerId);

            if (wholesaler == null)
            {
                return NotFound($"Wholesaler with id{wholesalerId} doesn't exist.");
            }

            var stringBuilder = new StringBuilder();
            foreach (var item in wholesaler.InventoryItems)
            {
                stringBuilder.AppendLine($"{wholesaler.Name} has {item.Stock} {item.BeerName} in stock.");
            }

            return Ok(stringBuilder.ToString());
        }

        [HttpPut]
        [Route("{wholesalerId:int}/{beerId:int}")]
        public async Task<ActionResult> UpdateStockCount(int wholesalerId, int beerId, [FromBody, Required] int count)
        {
            var wholesaler = await _context
                .Wholesalers
                .Include(w => w.InventoryItems)
                .FirstOrDefaultAsync(w => w.Id == wholesalerId);

            if (wholesaler == null)
            {
                return NotFound($"Wholesaler with id{wholesalerId} doesn't exist.");
            }

            var inventoryItem = wholesaler.InventoryItems.FirstOrDefault(i=>i.BeerId == beerId);

            if (inventoryItem == null)
            {
                return NotFound($"Beer with id {beerId} doesn't exist.");
            }

            inventoryItem.Stock = count;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("{wholesalerId:int}")]

        public async Task<ActionResult> GetQuotation(int wholesalerId, QuotationDto quotationDto)
        {
            if (!quotationDto.QuotationItemDtos.Any())
            {
                return BadRequest("Order can't be empty.");
            }

            var wholesaler = await _context
                .Wholesalers
                .Include(w => w.InventoryItems)
                .FirstOrDefaultAsync(w => w.Id == wholesalerId);
            if (wholesaler == null)
            {
                return NotFound($"Wholesaler with id {wholesalerId} doesn't exist.");
            }

            var notSellingBeers = quotationDto
                .QuotationItemDtos
                .Where(q => wholesaler.InventoryItems.Any(i => i.BeerId == q.BeerId))
                .Select(q => q.BeerId);
            if (notSellingBeers.Any())
            {
                return NotFound($"Beer with id(s) {{{string.Join(',', notSellingBeers)}}} are not sold by wholesaler");
            }

            var outOfStockBeers = quotationDto
                .QuotationItemDtos
                .Where(q => wholesaler.InventoryItems.Single(i => i.BeerId == q.BeerId).Stock == 0)
                .Select(q => q.BeerId);
            if (outOfStockBeers.Any())
            {
                return BadRequest($"Beer with id(s) {{{string.Join(',', outOfStockBeers)}}} are currently out of stock.");
            }

            var outOfRangeOrderBeers = quotationDto
                .QuotationItemDtos
                .Where(q => wholesaler.InventoryItems.Single(i => i.BeerId == q.BeerId).Stock < q.Quantity)
                .Select(q => q.BeerId);
            if (outOfRangeOrderBeers.Any())
            {
                return BadRequest($"Beer with id(s) {{{string.Join(',', outOfRangeOrderBeers)}}} are not sufficient enough in stock.");
            }

            var stringBuilder = new StringBuilder();

            var quotationItems = quotationDto.QuotationItemDtos.ToList();
            var orderCustomers = new List<OrderCustomer>();

            for (int i = 0; i < quotationItems.Count(); i++)
            {
                var inventoryItem = wholesaler
                    .InventoryItems
                    .Single(x => x.BeerId == quotationItems[i].BeerId);

                inventoryItem.Stock -= quotationItems[i].Quantity;
                orderCustomers.Add(new OrderCustomer()
                {
                    SNo = i +1,
                    Id = inventoryItem.BeerId,
                    Name=inventoryItem.BeerName,
                    Price = inventoryItem.Price,
                    Quantity = quotationItems[i].Quantity
                });
            }

            var builder = new StringBuilder("S.No\tId\tName\tPrice\tQuantity\tTotal");
            builder.AppendLine();
            var idx = 0;
            var summary = builder.AppendLine(string.Join(Environment.NewLine, orderCustomers));
            builder.AppendLine();

            var totalAmount = orderCustomers.Sum(a => a.Total);
            var totalItems = orderCustomers.Sum(a => a.Quantity);

            if (totalItems > 10)
            {
                var discountPercent = totalItems > 20 ? 20 : 10;
                var discountAmount = Math.Round((totalAmount * discountPercent) / 100, 2);

                totalAmount -=discountAmount;

                builder.AppendLine($"Discount: {discountAmount} ({discountPercent}%)");
                builder.AppendLine();
            }

            builder.AppendLine($"Total items: {totalItems}");
            builder.AppendLine($"Total amount: {totalAmount}");

            await _context.SaveChangesAsync();

            return Ok(builder.ToString());

        }

        internal record OrderCustomer
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public decimal Total => Math.Round( Price * Quantity, 2 );
            public int SNo { get; internal set; }

            public override string ToString()
            {
                return $"{SNo}\t{Id}\t{Name}\t{Price}\t{Quantity}\t{Total}";
            }
        }
    }
}
