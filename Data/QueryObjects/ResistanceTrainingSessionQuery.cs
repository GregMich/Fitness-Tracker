using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.DataTransferObjects;
using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Data.QueryObjects;

namespace Fitness_Tracker.Data.QueryObjects
{
    public static class ResistanceTrainingSessionQuery
    {
        public static IQueryable<IEnumerable<ResistanceTrainingSessionDTO>> GetResistanceTrainingSessionsDTOs(
            this IQueryable<User> user) =>
            user
            .Select(_ => _.ResistanceTrainingSessions
             .Select(rts => new ResistanceTrainingSessionDTO
            {
                UserId = rts.UserId,
                ResistanceTrainingSessionId = rts.ResistanceTrainingSessionId,
                TrainingSessionDate = rts.TrainingSessionDate,
                Excercises = rts.Excercises
                .Select(ex => new ExcerciseDTO
                {
                    ExcerciseId = ex.ExcerciseId,
                    ExcerciseName = ex.ExcerciseName,
                    ResistanceTrainingSessionId = ex.ResistanceTrainingSessionId,
                    Sets = ex.Sets
                        .Select(s => new SetDTO
                        {
                            SetId = s.SetId,
                            Reps = s.Reps,
                            Weight = s.Weight,
                            WeightUnit = s.WeightUnit,
                            RateOfPercievedExertion = s.RateOfPercievedExertion,
                            ExcerciseId = s.ExcerciseId
                        }).ToList()
                }).ToList()
            }));

        public static IQueryable<ResistanceTrainingSessionDTO> GetResistanceTrainingSessionDTOById(
            this IQueryable<ResistanceTrainingSession> resistanceTrainingSessions, int resistanceTrainingId, int userId) =>
            resistanceTrainingSessions
            .Where(rts =>
             rts.ResistanceTrainingSessionId == resistanceTrainingId
             && rts.UserId == userId)
            .Select(rts => new ResistanceTrainingSessionDTO
            {
                UserId = rts.UserId,
                ResistanceTrainingSessionId = rts.ResistanceTrainingSessionId,
                TrainingSessionDate = rts.TrainingSessionDate,
                Excercises = rts.Excercises
                .Select(ex => new ExcerciseDTO
                {
                    ExcerciseId = ex.ExcerciseId,
                    ExcerciseName = ex.ExcerciseName,
                    ResistanceTrainingSessionId = ex.ResistanceTrainingSessionId,
                    Sets = ex.Sets
                        .Select(s => new SetDTO
                        {
                            SetId = s.SetId,
                            Reps = s.Reps,
                            Weight = s.Weight,
                            WeightUnit = s.WeightUnit,
                            RateOfPercievedExertion = s.RateOfPercievedExertion,
                            ExcerciseId = s.ExcerciseId
                        }).ToList()
                }).ToList()
            });

        public static IQueryable<ResistanceTrainingSession> GetResistanceTrainingSessionByIdForDeletion(
            this IQueryable<ResistanceTrainingSession> resistanceTrainingSessions, int resistanceTrainingId, int userId) =>
            resistanceTrainingSessions
            .Where(rts =>
             rts.ResistanceTrainingSessionId == resistanceTrainingId
             && rts.UserId == userId);
    }
}
