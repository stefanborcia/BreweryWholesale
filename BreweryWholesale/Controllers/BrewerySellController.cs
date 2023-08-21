using BreweryWholesale.Data;
using BreweryWholesale.Data.Entities;
using BreweryWholesale.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Controllers
{
    [Route("api/brewery/{breweryId:int}/sell/{wholesalerId:int}")]
    [ApiController]
    public class BrewerySellController : ControllerBase
    {
        private readonly BreweryContext _context;

        public BrewerySellController(BreweryContext context)
        {
            _context = context;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> SellBeers(int breweryId, int wholesalerId, WholesalerBuyDto wholesalerBuyDto)
        {
            var wholesaler = await _context
                .Wholesalers
                .Include(w => w.InventoryItems)
                .FirstOrDefaultAsync(w => w.Id == wholesalerId);

            if (wholesaler == null)
            {
                return NotFound($"Wholesaler with id{wholesalerId} doesn't exist.");
            }

            var brewery = await _context
                .Breweries
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(b => b.Id == breweryId);

            if (brewery == null)
            {
                return NotFound($"Brewery with id{breweryId} doesn't exist.");
            }

            var notAvailableBeers = wholesalerBuyDto
                .OrderItems
                .Where(o => !brewery.Beers.Any(b => b.Id == o.BeerId))
                .Select(o => o.BeerId);

            if (notAvailableBeers.Any())
            {
                return NotFound(
                    $"Beers with id {{string.Join(',', notAvailableBeers)}} is not being sold by Brewery {breweryId}");
            }

            var beers = wholesalerBuyDto
                .OrderItems
                .Select(o =>
                {
                    var beer = brewery.Beers.Single(b => b.Id == o.BeerId);
                    var inventory = wholesaler
                        .InventoryItems
                        .FirstOrDefault(i => i.BeerId == o.BeerId);

                    if (inventory != null)
                    {
                        inventory.Stock += o.Quantity;
                        return inventory;
                    }
                    else
                    {
                        return new Inventory()
                        {
                            BeerId = beer.Id,
                            BeerName = beer.Name,
                            Price = beer.Price,
                            Wholesaler = wholesaler,
                            Stock = o.Quantity
                        };
                    }
                });

            var addedBeers = beers.Where(b=>b.WholesalerId == 0).ToList();
            addedBeers.ForEach(ab =>wholesaler.InventoryItems.Add(ab));

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
