using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Data.Entities.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base (options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ResistanceTrainingSession> ResistanceTrainingSessions { get; set; }
        public DbSet<CardioSession> CardioSessions { get; set; }
        public DbSet<NutritionTarget> NutritionTargets { get; set; }
        public DbSet<DailyNutritionLog> DailyNutritionLogs { get; set; }
        public DbSet<Stats> Stats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SetEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StatsEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DailyNutritionLogEntityConfiguration());
        }
    }
}
