using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DAL.Config.SystemConfig
{
    public class ReviewConfiguration : BaseEntityConfigurations<Review>
    {
        public override void Configure(EntityTypeBuilder<Review> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.UnitComment).HasColumnName("unit_comment").HasColumnType(DBTypes.NVARCHARMAX);
            builder.Property(r => r.HotelComment).HasColumnName("hotel_comment").HasColumnType(DBTypes.NVARCHARMAX);
            builder.Property(r => r.HotelRate).HasColumnName("hotel_rate").HasColumnType(DBTypes.INT);
            builder.Property(r => r.UnitRate).HasColumnName("unit_rate").HasColumnType(DBTypes.INT);
            builder.Property(r => r.ClientId).HasColumnName("client_id").HasColumnType(DBTypes.NVARCHAR_36);
            builder.Property(r => r.UnitId).HasColumnName("unit_id").HasColumnType(DBTypes.NVARCHAR_36);
            builder.HasOne(r => r.Client).WithMany(c => c.Reviews).HasForeignKey(r => r.ClientId);
            builder.HasOne(r => r.Hotel).WithMany().HasForeignKey(r => r.HotelId);
        }
    }
}
