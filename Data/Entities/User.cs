using System;
using System.Collections.Generic;

namespace Fitness_Tracker.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string PasswordHash { get; set; }

        // entity relationships
        public List<ResistanceTrainingSession> ResistanceTrainingSessions { get; set; }
        public List<CardioSession> CardioSessions { get; set; }
    }
}
