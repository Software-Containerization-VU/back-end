using InventoryAPI.DbContexts;
using InventoryAPI.Helpers;
using InventoryAPI.Seeds;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace InventoryAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            var connectionString = "Host=" + Environment.GetEnvironmentVariable("INVENTORYSERVICE_DB_HOST") + ";" +
                                "Database=" + Environment.GetEnvironmentVariable("INVENTORYSERVICE_DB_NAME") + ";" +
                                "Username=" + Environment.GetEnvironmentVariable("INVENTORYSERVICE_DB_USER") + ";" +
                                "Password=" + Environment.GetEnvironmentVariable("INVENTORYSERVICE_DB_PASSWORD") + ";" +
                                "Port=" + Environment.GetEnvironmentVariable("INVENTORYSERVICE_DB_PORT") + ";";

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InventoryDBConnectionString")));


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Seed the db on init
            //context.Seed();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MigrationHelper migrationHelper = new MigrationHelper();
            migrationHelper.Migrate(app);
            //migration.ApplyMigrations(context);
        }
    }
}
