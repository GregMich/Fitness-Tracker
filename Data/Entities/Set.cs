using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Data.Entities
{
    public class Set
    {
        public int SetId { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public int? RateOfPercievedExertion { get; set; }
         
        // entity relationships
        public Excercise Excercise { get; set; }
        public int ExcerciseId { get; set; }
    }

    public enum WeightUnit
    {
        Pounds, Kilograms
    }
}
