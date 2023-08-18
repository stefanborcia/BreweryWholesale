﻿using BreweryWholesale.Data;
using BreweryWholesale.Data.Entities;
using BreweryWholesale.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Brewerys
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
                .Brewerys
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
    }
}
