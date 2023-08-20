using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;
using BreweryWholesale.Data;
using BreweryWholesale.Data.Entities;
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
    }
}
