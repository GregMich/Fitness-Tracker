using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Data.Entities
{
    public class Stats
    {
        // foreign key
        public int UserId { get; set; }
        // primary key
        public int StatsId { get; set; }
        public decimal Weight { get; set; }
        public int HeightFeet { get; set; }
        public int HeightInch { get; set; }
        public int Age { get; set; }
        public decimal BodyfatPercentage { get; set; }
        public BodyweightUnit WeightUnit { get; set; }

        // entity relationships
        public User User { get; set; }
    }

    public enum BodyweightUnit
    {
        Lb, Kg
    }
}
