using Microsoft.EntityFrameworkCore;
using OPG.Models;

namespace OPG
{
    public class OPGDbContext : DbContext
    {
        public OPGDbContext(DbContextOptions<OPGDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
