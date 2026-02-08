using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DAL.Config.SystemConfig
{
    public class HotelConfiguration : BaseEntityConfigurations<Hotel>
    {
        public override void Configure(EntityTypeBuilder<Hotel> builder)
        {
            base.Configure(builder);

            builder.Property(h => h.Name).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(200).IsRequired();
            builder.Property(h => h.Description).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(400);
            builder.Property(h => h.Street).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(100);
            builder.Property(h => h.City).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(100);
            builder.Property(h => h.Governate).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(100);
            builder.Property(h => h.HotelStatus).HasColumnType(DBTypes.INT);


            builder.HasMany(h => h.SeasonalPricings)
                   .WithMany(s => s.Hotels)
                   .UsingEntity(j => j.ToTable("hotel_seasonal_pricings"));  // <-- Shadow table because relation M to M 
        }
    }

}
