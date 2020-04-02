using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fitness_Tracker.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base (options) { }

        public DbSet<User> Users { get; set; }
    }
}
