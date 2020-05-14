using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Fitness_Tracker.Data.Startup;
using Microsoft.Extensions.Logging;

namespace Fitness_Tracker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
            .Build()
            .MigrateDatabase()
            .SeedDatabase()
            .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseDefaultServiceProvider(options =>
                    options.ValidateScopes = false);
    }
}
