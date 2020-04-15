using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Data.Entities
{
    public class CardioSession
    {
        public int CardioSessionId { get; set; }
        public DateTime CardioSessionDate { get; set; }
        public int Duration { get; set; }

        // entity relationships
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
