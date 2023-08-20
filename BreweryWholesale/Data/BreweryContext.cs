using BreweryWholesale.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Data
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Inventory>(i =>
            {
                i.HasKey(x => new { x.BeerId, x.WholesalerId });
            });
        }
        public DbSet<Brewery> Brewerys { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }

    }
}
