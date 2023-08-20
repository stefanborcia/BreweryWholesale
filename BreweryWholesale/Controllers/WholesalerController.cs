using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using BreweryWholesale.Data;
using BreweryWholesale.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
