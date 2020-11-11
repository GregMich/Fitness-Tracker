using System;
using System.Collections.Generic;
using Fitness_Tracker.Data.Entities;

namespace Fitness_Tracker.Data.DataTransferObjects
{
    public class DailyNutritionLogDTO
    {
        public int DailyNutritionLogId { get; set; }
        public int UserId { get; set; }
        public DateTime NutritionLogDate { get; set; }
        public IEnumerable<FoodEntryDTO> FoodEntries { get; set; }
    }
}
