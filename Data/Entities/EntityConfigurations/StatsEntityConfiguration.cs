using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness_Tracker.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fitness_Tracker.Data.Entities.EntityConfigurations
{
    public class StatsEntityConfiguration: IEntityTypeConfiguration<Stats>
    {
        public void Configure(EntityTypeBuilder<Stats> entity)
        {
            entity.Property(_ => _.WeightUnit)
                .HasConversion(new EnumToStringConverter<BodyweightUnit>());

            entity.Property(_ => _.BodyfatPercentage)
                .HasColumnType("decimal(5,2)");

            entity.Property(_ => _.Weight)
                .HasColumnType("decimal(6,2)");
        }
    }
}
