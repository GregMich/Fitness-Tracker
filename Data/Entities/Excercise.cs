using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Data.Entities
{
    public class Excercise
    {
        public int ExcerciseId { get; set; }
        public string ExcerciseName { get; set; }

        // entity relationships
        public List<Set> Sets { get; set; }

        public ResistanceTrainingSession ResistanceTrainingSession { get; set; }
        public int ResistanceTrainingSessionId { get; set; }

    }
}
