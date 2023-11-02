using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BattleCottage.Data;
using BattleCottage.Tests;

namespace BattleCottage.Services.Tests
{
    public class ServicesWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove ApplicationDbContext
                ServiceDescriptor? descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>)
                );

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseNpgsql(
                        "Host=localhost; Database=thimylife_test; Username=BattleCottage_admin; Password=foobar123"
                    );
                });

                // Ensure schema gets created
                ServiceProvider serviceProvider = services.BuildServiceProvider();

                using IServiceScope scope = serviceProvider.CreateScope();

                IServiceProvider scopedServices = scope.ServiceProvider;

                ApplicationDbContext context = scopedServices.GetRequiredService<ApplicationDbContext>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                services.AddScoped<IDatabaseOperations, DatabaseOperations>();
            });

            builder.UseEnvironment("Development");
        }
    }
}
