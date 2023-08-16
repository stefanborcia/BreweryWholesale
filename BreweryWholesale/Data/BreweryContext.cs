using Microsoft.EntityFrameworkCore;

namespace BreweryWholesale.Data
{
    public class BreweryContext : DbContext
    {
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
        {

        }
    }
}
