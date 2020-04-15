using System;
using System.Collections.Generic;

namespace Fitness_Tracker.Data.Entities
{
    public class ResistanceTrainingSession
    {
        public int ResistanceTrainingSessionId { get; set; }
        public DateTime TrainingSessionDate { get; set; }

        // entity relationships
        public List<Excercise> Excercises { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
