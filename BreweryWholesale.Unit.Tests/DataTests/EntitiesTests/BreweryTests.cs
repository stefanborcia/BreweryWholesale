using BreweryWholesale.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using BreweryWholesale.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Unit.Tests.DataTests.EntitiesTests
{
    public class BreweryTests
    {
        private readonly BreweryContext _context;

        public BreweryTests()
        {
            var options = new DbContextOptionsBuilder<BreweryContext>()
                .UseInMemoryDatabase(nameof(BreweryTests) + DateTime.Now.Ticks)
                .Options;
            _context = new BreweryContext(options);
        }

        [Fact]
        public void Add_Brewery_Should_Save()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory"
            };
            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery!.Id.Should().BeGreaterOrEqualTo(1);
            dbBrewery.Name.Should().Be(brewery.Name);
        }

        [Fact]
        public void Delete_Brewery_Should_Remove_From_Database()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory"
            };
            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery.Should().NotBeNull();
            _context.Breweries.Remove(brewery);
            _context.SaveChanges();

            dbBrewery= _context.Breweries.FirstOrDefault();
            dbBrewery.Should().BeNull();
        }

        [Fact]
        public void Update_Brewery_Should_Update_From_Database()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory"
            };

            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context.Breweries.FirstOrDefault();

            dbBrewery.Should().NotBeNull();
            dbBrewery!.Name = "Update name";
            _context.SaveChanges();

            dbBrewery=_context.Breweries.FirstOrDefault();
            dbBrewery!.Name.Should().Be("Update name");
        }

        [Fact]
        public void Add_Brewery_With_Beers_Should_Add()
        {
            var brewery = new Brewery()
            {
                Name = "Beer Factory"
            };
            var beer = new Beer()
            {
                Name = "Duvel",
                Price=2.5m,
                AlcoholContent = 8.5m,
                Brewery = brewery
            };
            brewery.Beers.Add(beer);

            _context.Breweries.Add(brewery);
            _context.SaveChanges();

            var dbBrewery = _context
                .Breweries
                .Include(b => b.Beers)
                .FirstOrDefault();

          var dbBeer=dbBrewery.Beers.FirstOrDefault();
          dbBeer.Should().NotBeNull();

          dbBeer.Id.Should().BeGreaterOrEqualTo(1);
          dbBeer.Name.Should().Be(beer.Name);
          dbBeer.Price.Should().Be(beer.Price);
          dbBeer.AlcoholContent.Should().Be(beer.AlcoholContent);
        }
    }

}
