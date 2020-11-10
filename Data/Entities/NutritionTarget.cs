namespace Fitness_Tracker.Data.Entities
{
    public class NutritionTarget
    {
        // primary key
        public int NutritionTargetId { get; set; }
        // foreign key
        public int UserId { get; set; }
        public User User { get; set; }
        public int CalorieTarget { get; set; }
        // TOOD add other metrics such as macro and micro nutrient targets
    }
}