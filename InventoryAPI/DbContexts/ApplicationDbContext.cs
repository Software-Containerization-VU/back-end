
using Microsoft.EntityFrameworkCore;
using InventoryAPI.Models;

namespace InventoryAPI.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
          : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
