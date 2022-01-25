using InventoryAPI.DbContexts;
using InventoryAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventoryAPI.Seeds
{
    public static class ProductSeeds
    {
        public static IEnumerable<Product> Get()
        {
            return new List<Product>()
            {
                new Product{ ProductName = "Miso Soup", ProductPrice = 15},
                new Product{ ProductName = "Chocolate", ProductPrice = 23  },
                new Product{ ProductName = "Vanilla", ProductPrice = 99},
            };
        }
        public static void Seed(this ApplicationDbContext dbContext)
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Product
                {
                    ProductName = "Miso Soup",
                    ProductPrice = 23
                });
                dbContext.Products.Add(new Product
                {
                    ProductName = "Vanila",
                    ProductPrice = 45

                });
                dbContext.Products.Add(new Product
                {
                    ProductName = "Chocolate",
                    ProductPrice = 99

                });

                dbContext.SaveChanges();
            }
        }
    }
}
