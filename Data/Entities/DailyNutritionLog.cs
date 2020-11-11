using System;
using System.Collections.Generic;

namespace Fitness_Tracker.Data.Entities
{
    public class DailyNutritionLog
    {
        // primary key
        public int DailyNutritionLogId { get; set; }
        // foreign key
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime NutritionLogDate { get; set; }
        public IEnumerable<FoodEntry> FoodEntries { get; set; }
    }
}