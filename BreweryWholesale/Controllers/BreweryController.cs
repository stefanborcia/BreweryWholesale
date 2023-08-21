using BreweryWholesale.Data;
using BreweryWholesale.Data.Entities;
using BreweryWholesale.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BreweryWholesale.Controllers
{
    [Route("api/brewery/{breweryId:int}")]
    [ApiController]
    public class BreweryController : ControllerBase
    {
        private readonly BreweryContext _context;
        public BreweryController(BreweryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Brewery>>> GetAllBeers(int breweryId)
        {
            var brewery = await _context
                .Breweries
                .Include(b=>b.Beers)
                .FirstOrDefaultAsync(b=>b.Id == breweryId);
            if (brewery == null)
            {
                return NotFound($"Brewery with id: {breweryId} we don't found.");
            }

            var beers = BeerDto.BeerDtos(brewery.Beers);

            return Ok(beers);
        }

        [HttpPost]
        public async Task<ActionResult> AddBeer(int breweryId, BeerDto beerDto)
        {
            var brewery = await _context
                .Breweries
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(b => b.Id == breweryId);

            if (brewery == null)
            {
                return NotFound($"Brewery with id: {breweryId} we don't found.");
            }

            var beer = beerDto.ToBeer(brewery);
            brewery.Beers.Add(beer);
            await _context.SaveChangesAsync();
            var uri = $"api/brewery/{breweryId}/beer/{beer.Id}";

            return Created(uri, BeerDto.From(beer));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBeer(int breweryId, int beerId)
        {
            var brewery = await _context
                .Breweries
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(b => b.Id == breweryId);

            var beer = brewery.Beers.FirstOrDefault(b => b.Id == beerId);
            if (beer == null)
            {
                return NotFound($"The beer with id{beerId} doesn't exist.");
            }

            brewery.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
