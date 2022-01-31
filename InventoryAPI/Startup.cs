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
        //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionString = "Host=" + Environment.GetEnvironmentVariable("POSTGRES_HOST") + ";" +
                                "Database=" + Environment.GetEnvironmentVariable("POSTGRES_DB") + ";" +
                                "Username=" + Environment.GetEnvironmentVariable("POSTGRES_USER") + ";" +
                                "Password=" + Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") + ";" +
                                "Port=" + Environment.GetEnvironmentVariable("POSTGRES_PORT") + ";";    

            //var connectionString = "Host=localhost;Database=guru99;Username=postgres;Password=316134;Port=5432;";

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));



            services.AddCors(options =>
            {

                options.AddPolicy("CORSPolicy",
                builder =>
                {
                    builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => true);
                    //.AllowCredentials();
                });

                //options.AddPolicy(name: MyAllowSpecificOrigins,
                //                  builder =>
                //                  {
                //                      builder.WithOrigins("https://lkrumpak.com");
                //                  });
            }); 
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CORSPolicy");

            //app.UseCors(MyAllowSpecificOrigins); 

            //// global cors policy
            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials()); // allow 


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MigrationHelper migrationHelper = new MigrationHelper();
            //migrationHelper.Migrate(app);
            migrationHelper.ApplyMigrations(context);

            //Seed the db on init
            context.Seed();
        }
    }
}
