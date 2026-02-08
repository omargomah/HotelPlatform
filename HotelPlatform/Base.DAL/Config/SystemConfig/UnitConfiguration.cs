using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Base.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Base.DAL.Config.SystemConfig
{
    public class UnitConfiguration : BaseEntityConfigurations<Unit>
    {
        public override void Configure(EntityTypeBuilder<Unit> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.BasePrice)
                .HasColumnName("base_price")
                .IsRequired()
                .HasColumnType(DBTypes.DECIMAL18_4);

            builder.Property(u => u.RoomNumber)
                .HasColumnName("room_number")
                .IsRequired()
                .HasColumnType(DBTypes.INT);

            builder.Property(u => u.UnitType)
                .HasColumnName("unit_type")
                .HasConversion(new EnumToStringConverter<UnitType>())
                .HasMaxLength(6)
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR);

            builder.Property(u => u.UnitStatus)
                .HasColumnName("unit_status")
                .HasConversion(new EnumToStringConverter<UnitStatus>())
                .HasMaxLength(9)
                .HasDefaultValue(UnitStatus.Closed.ToString())
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR);

            builder.Property(u => u.Description)
                .HasColumnName("description")
                .HasMaxLength(400)
                .IsRequired(false)
                .HasColumnType(DBTypes.NVARCHAR);

            builder.Property(u => u.HotelId)
                .HasColumnName("hotel_id")
                .IsRequired()
                .HasColumnType(DBTypes.NVARCHAR_36);

            builder.HasMany(h => h.UnitPhotos)
                .WithOne(p => p.Unit)
                .HasForeignKey(p => p.UnitId);

            builder.HasOne(u => u.Hotel)
                   .WithMany(h => h.Units)
                   .HasForeignKey(u => u.HotelId);
        }
    }

}
