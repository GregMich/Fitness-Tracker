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
                    if(!context.Users.Any() && environment.IsDevelopment())
                    {
                        var seededUsers = new List<User>
                        {
                            new User
                            {
                                FirstName = "Greg",
                                LastName = "Michael",
                                Username = "gmichael",
                                Email = "gmichael@gmail.com",
                                BirthDate = new DateTime(1995, 9, 15),
                                PasswordHash = PasswordSecurity.HashPassword("secret"),
                                Stats = new Stats
                                {
                                    UserId = 1,
                                    Weight = 225,
                                    HeightFeet = 6,
                                    HeightInch = 1,
                                    Age = 24,
                                    BodyfatPercentage = 18,
                                    WeightUnit = BodyweightUnit.Lb
                                },
                                ResistanceTrainingSessions = new List<ResistanceTrainingSession>
                                {
                                    new ResistanceTrainingSession
                                    {
                                        TrainingSessionDate = new DateTime(year: 2020, month: 5, day: 22),
                                        Excercises = new List<Excercise>
                                        {
                                            new Excercise
                                            {
                                                ExcerciseName = "Bench Press",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 225,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 220,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 215,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 210,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    }
                                                }
                                            },
                                            new Excercise
                                            {
                                                ExcerciseName = "Squat",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 315,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 305,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 295,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 270,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                }
                                            },
                                            new Excercise
                                            {
                                                ExcerciseName = "Row",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 155,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 145,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 135,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 135,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                }
                                            }
                                        }
                                    },
                                    new ResistanceTrainingSession
                                    {
                                        TrainingSessionDate = new DateTime(year: 2020, month: 5, day: 20),
                                        Excercises = new List<Excercise>
                                        {
                                            new Excercise
                                            {
                                                ExcerciseName = "Bench Press",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 225,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 220,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 215,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 210,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    }
                                                }
                                            },
                                            new Excercise
                                            {
                                                ExcerciseName = "Squat",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 315,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 305,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 295,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 270,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                }
                                            },
                                            new Excercise
                                            {
                                                ExcerciseName = "Row",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 155,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 145,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 135,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 135,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                }
                                            }
                                        }
                                    }
                                }
                            },
                            new User
                            {
                                FirstName = "John",
                                LastName = "Moeller",
                                Username = "jmoeller",
                                Email = "jmoeller@test.com",
                                BirthDate = new DateTime(1990, 12, 23),
                                PasswordHash = PasswordSecurity.HashPassword("secret2"),
                                ResistanceTrainingSessions = new List<ResistanceTrainingSession>
                                {
                                    new ResistanceTrainingSession
                                    {
                                        TrainingSessionDate = new DateTime(year: 2020, month: 5, day: 22),
                                        Excercises = new List<Excercise>
                                        {
                                            new Excercise
                                            {
                                                ExcerciseName = "Push Press",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 1,
                                                        Weight = 225,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 1,
                                                        Weight = 220,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 1,
                                                        Weight = 215,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 1,
                                                        Weight = 210,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 9
                                                    }
                                                }
                                            },
                                            new Excercise
                                            {
                                                ExcerciseName = "Deadlift",
                                                Sets = new List<Set>
                                                {
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 315,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 305,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 295,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                    new Set
                                                    {
                                                        Reps = 8,
                                                        Weight = 270,
                                                        WeightUnit = WeightUnit.Pounds,
                                                        RateOfPercievedExertion = 8
                                                    },
                                                }
                                            }
                                        }
                                    }
                                }
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
