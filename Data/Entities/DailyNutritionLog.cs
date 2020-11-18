using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fitness_Tracker.Data.Entities
{
    public class DailyNutritionLog
    {
        // primary key
        public int DailyNutritionLogId { get; set; }
        // foreign key
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public DateTime NutritionLogDate { get; set; }
        public IList<FoodEntry> FoodEntries { get; set; }
    }
}