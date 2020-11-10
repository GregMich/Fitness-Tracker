using System.Linq;
using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Data.DataTransferObjects;

namespace Fitness_Tracker.Data.QueryObjects
{
    public static class NutritionTargetQuery
    {
        public static IQueryable<NutritionTargetDTO> GetNutritionTarget(
            this IQueryable<NutritionTarget> nutritionTargets) =>
                nutritionTargets
                .Select(nutritionTarget => new NutritionTargetDTO
                {
                    NutritionTargetId = nutritionTarget.NutritionTargetId,
                    UserId = nutritionTarget.UserId,
                    CalorieTarget = nutritionTarget.CalorieTarget
                });
    }
}
