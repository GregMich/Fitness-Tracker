namespace Fitness_Tracker.Data.Entities
{
    public class FoodEntry
    {
        // primary key
        public int FoodEntryId { get; set; }
        // foreign key
        public int DailyNutritionLogId { get; set; }
        public DailyNutritionLog DailyNutritionLog { get; set; }
        public int Calories { get; set; }
        // TODO add additional metrics including micro and macro nutrients
    }
}