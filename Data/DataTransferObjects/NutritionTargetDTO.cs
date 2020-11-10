using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Data.DataTransferObjects
{
    public class NutritionTargetDTO
    {
        public int NutritionTargetId { get; set; }
        public int UserId { get; set; }
        public int CalorieTarget { get; set; }
    }
}
