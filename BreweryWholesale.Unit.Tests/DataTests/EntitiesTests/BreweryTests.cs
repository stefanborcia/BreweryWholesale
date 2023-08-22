using BreweryWholesale.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .UseInMemoryDatabase(nameof(BreweryTests))
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
    }

}
