using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DAL.Config.SystemConfig
{
    public class BookingConfiguration : BaseEntityConfigurations<Booking>
    {
        public override void Configure(EntityTypeBuilder<Booking> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.StartDate).HasColumnName("start_date").HasColumnType(DBTypes.DATETIME).IsRequired();
            builder.Property(b => b.EndDate).HasColumnName("end_date").HasColumnType(DBTypes.DATETIME).IsRequired();
            builder.Property(b => b.TotalPrice).HasColumnName("total_price").HasColumnType(DBTypes.DECIMAL18_4);
            builder.Property(b => b.UnitId).HasColumnName("unit_id").HasColumnType(DBTypes.NVARCHAR_36).IsRequired();

            builder.HasOne(b => b.Unit)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(b => b.UnitId);

            builder.HasOne(b => b.Client)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(b => b.CreatedById);
        }
    }
}
