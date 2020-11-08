using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fitness_Tracker.Data.Entities.EntityConfigurations
{
    public class SetEntityConfiguration: IEntityTypeConfiguration<Set>
    {
        public void Configure(EntityTypeBuilder<Set> entity)
        {
            entity.Property(_ => _.WeightUnit)
                .HasConversion(new EnumToStringConverter<WeightUnit>());

            entity.Property(_ => _.Weight)
                .HasColumnType("decimal(6,2)");
        }
    }
}
