using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Entities;
using Fitness_Tracker.Data.DataTransferObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fitness_Tracker.Data.QueryObjects
{
    public static class DailyNutritionLogQuery
    {
        public static IQueryable<DailyNutritionLogDTO> GetDailyNutritionLogDTOs(
            this IQueryable<DailyNutritionLog> dailyNutritionLogs) =>
            dailyNutritionLogs.Select(dnr => new DailyNutritionLogDTO
            {
                DailyNutritionLogId = dnr.DailyNutritionLogId,
                UserId = dnr.UserId,
                NutritionLogDate = dnr.NutritionLogDate,
                FoodEntries = dnr.FoodEntries.Select(fe => new FoodEntryDTO
                {
                    FoodEntryId = fe.FoodEntryId,
                    DailyNutritionLogId = fe.DailyNutritionLogId,
                    Calories = fe.Calories
                }).ToList()
            });

        public static IQueryable<DailyNutritionLogDTO> OrderDailyNutritionLogDTOsBy(
            this IQueryable<DailyNutritionLogDTO> dailyNutritionLogDTOs,
            DailyNutritionLogSortingOption sortingOption)
        {
            switch (sortingOption)
            {
                case DailyNutritionLogSortingOption.mostRecent:
                    return dailyNutritionLogDTOs.OrderByDescending(_ => _.NutritionLogDate);

                case DailyNutritionLogSortingOption.leastRecent:
                    return dailyNutritionLogDTOs.OrderBy(_ => _.NutritionLogDate);

                default:
                    throw new ArgumentException("A valid ordering option must be supplied");
            }
        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DailyNutritionLogSortingOption
    {
        mostRecent,
        leastRecent
    }
}
