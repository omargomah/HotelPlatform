using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DAL.Config.SystemConfig
{
    public class SeasonalPricingConfiguration : BaseEntityConfigurations<SeasonalPricing>
    {
        public override void Configure(EntityTypeBuilder<SeasonalPricing> builder)
        {
            base.Configure(builder);
            
            builder.Property(s => s.Name)
                .HasColumnName("name")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(s => s.IncreasingPercentage)
                .HasColumnName("increasing_percentage")
                .HasColumnType(DBTypes.DECIMAL18_4)
                .IsRequired();

            builder.Property(s => s.Start)
                .HasColumnName("star")
                .HasColumnType(DBTypes.DATETIME)
                .IsRequired();

            builder.Property(s => s.End)
                .HasColumnName("end")
                .HasColumnType(DBTypes.DATETIME)
                .IsRequired();
        }
    }
}
