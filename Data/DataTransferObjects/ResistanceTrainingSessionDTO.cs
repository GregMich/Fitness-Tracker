using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Entities;

namespace Fitness_Tracker.Data.DataTransferObjects
{
    public class ResistanceTrainingSessionDTO
    {
        public int ResistanceTrainingSessionId { get; set; }
        public DateTime TrainingSessionDate { get; set; }
        public List<ExcerciseDTO> Excercises { get; set; }
        public int UserId { get; set; }
    }
}
