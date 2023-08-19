using BreweryWholesale.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BreweryWholesale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewerySellController : ControllerBase
    {
        private readonly BreweryContext _context;

        public BrewerySellController(BreweryContext context)
        {
            _context = context;
        }

    }
}
