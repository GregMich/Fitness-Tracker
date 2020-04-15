using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SetEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
