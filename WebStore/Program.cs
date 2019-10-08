using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebStore.Data;
using WebStore.DAL.Context;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services
                        .GetRequiredService<WebStoreContext>();
                    DbInitializer.Initialize(context);
                    DbInitializer.InitializeUsers(services);
                }
                catch (Exception exception)
                {
                    var logger = services
                        .GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception,
                        "An error occurred while seeding " +
                        "the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
