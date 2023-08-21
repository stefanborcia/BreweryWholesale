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

            modelBuilder.Entity<Beer>().Property(p => p.AlcoholContent).HasPrecision(10, 2);
            modelBuilder.Entity<Beer>().Property(p=>p.Price).HasPrecision(10, 2);
            modelBuilder.Entity<Inventory>().Property(p => p.Price).HasPrecision(10, 2);
        }
        public DbSet<Brewery> Brewerys { get; set; }
        public DbSet<Wholesaler> Wholesalers { get; set; }

    }
}
