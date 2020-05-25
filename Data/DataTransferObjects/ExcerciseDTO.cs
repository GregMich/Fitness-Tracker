using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Entities;

namespace Fitness_Tracker.Data.DataTransferObjects
{
    public class ExcerciseDTO
    {
        public int ExcerciseId { get; set; }
        public string ExcerciseName { get; set; }
        public List<SetDTO> Sets { get; set; }
        public int ResistanceTrainingSessionId { get; set; }

    }
}
