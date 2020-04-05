using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Fitness_Tracker.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Infrastructure.PasswordSecurity;
using Microsoft.Extensions.Hosting;

namespace Fitness_Tracker.Data.Startup
{
    public static class DatabaseStartupFunctions
    {

        /*
         * Performs database migrations in EF Core upon startup of the application, may
         * not be the most ideal solution, may want to revert to running migrations as part
         * of deployment process instead of automatically
         */
        public static IWebHost MigrateDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var context = services.GetRequiredService<ApplicationDbContext>())
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return webHost;
        }

        /*
         * Seeds the database with example data when the database tables are empty
         * and the application is in a development environment. As database tables are
         * added to the relational database schema, this code will need to be updated to
         * 1) check that the database tables are empty, and 2) seed the tables with data.
         * 
         * TODO future improvements to this static method should include a method for
         * generating data when in a test environment for integration testing
         */
        public static IWebHost SeedDatabase(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var environment = services.GetRequiredService<IHostEnvironment>();

                using (var context = services.GetRequiredService<ApplicationDbContext>())
                {
                    if(!context.Users.Any())
                    {
                        var seededUsers = new List<User>
                        {
                            new User
                            {
                                FirstName = "Greg",
                                LastName = "Mich",
                                Username = "gmich",
                                Email = "gregmich95@gmail.com",
                                BirthDate = new DateTime(1995, 9, 15),
                                PasswordHash = PasswordSecurity.HashPassword("secret")
                            },
                            new User
                            {
                                FirstName = "John",
                                LastName = "Moeller",
                                Username = "jmoeller",
                                Email = "jmoeller@test.com",
                                BirthDate = new DateTime(1990, 12, 23),
                                PasswordHash = PasswordSecurity.HashPassword("secret2")
                            },
                            new User
                            {
                                FirstName = "Marcus",
                                LastName = "Fenix",
                                Username = "mfenix",
                                Email = "mfenix@test.com",
                                BirthDate = new DateTime(1999, 12, 12),
                                PasswordHash = PasswordSecurity.HashPassword("secret3")
                            }
                        };

                        context.AddRange(seededUsers);
                        context.SaveChanges();
                    }
                }
            }
            return webHost;
        }
    }
}
