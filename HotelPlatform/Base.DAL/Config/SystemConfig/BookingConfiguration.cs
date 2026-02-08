using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Base.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
            builder.Property(b => b.CountOfPerson)
                .HasColumnName("count_of_person")
                .HasColumnType(DBTypes.INT)
                .IsRequired();

            builder.Property(b => b.BookingStatus)
                .HasColumnName("booking_status")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(10)
                .HasConversion(new EnumToStringConverter<BookingStatus>())
                .HasDefaultValue(BookingStatus.Pending.ToString())
                .IsRequired();

            builder.HasOne(b => b.Unit)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(b => b.UnitId);

            builder.HasOne(b => b.Client)
                   .WithMany(u => u.Bookings)
                   .HasForeignKey(b => b.CreatedById);
        }
    }
}
