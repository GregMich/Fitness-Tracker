using Fitness_Tracker.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Data.DataTransferObjects
{
    public class SetDTO
    {
        public int SetId { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public int? RateOfPercievedExertion { get; set; }
        public int ExcerciseId { get; set; }
    }
}
