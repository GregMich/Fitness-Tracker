using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.DataTransferObjects;

namespace Fitness_Tracker.Data.DataTransferObjects
{
    public class FoodEntryDTO
    {
        public int FoodEntryId { get; set; }
        public int DailyNutritionLogId { get; set; }
        public int Calories { get; set; }
    }
}
