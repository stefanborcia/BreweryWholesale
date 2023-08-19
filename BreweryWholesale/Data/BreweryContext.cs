using BreweryWholesale.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Data
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
        {

        }
        public DbSet<Brewery> Brewerys { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }

    }
}
