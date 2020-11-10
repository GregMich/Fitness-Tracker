using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fitness_Tracker.Data.Entities.EntityConfigurations
{
    public class DailyNutritionLogEntityConfiguration: IEntityTypeConfiguration<DailyNutritionLog>
    {
        public void Configure(EntityTypeBuilder<DailyNutritionLog> entity)
        {
            entity.Property(_ => _.NutritionLogDate)
                .HasColumnType("date");
        }
    }
}
