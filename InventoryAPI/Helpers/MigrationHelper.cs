using InventoryAPI.DbContexts;
using InventoryAPI.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace InventoryAPI.Helpers
{
    public class MigrationHelper
    {
        public void ApplyMigrations(ApplicationDbContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        public void Migrate(IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            ctx.Database.Migrate();

            AddProducts(ctx);
        }

        private void AddProducts(ApplicationDbContext ctx)
        {
            foreach (var crs in ProductSeeds.Get())
            {
                var tmp = ctx.Products
                    .Where(c => c.ProductId == crs.ProductId)
                    .FirstOrDefault();
                if (tmp == null)
                {
                    ctx.Products.Add(crs);
                }
            }
            ctx.SaveChanges();
        }
    }
}
