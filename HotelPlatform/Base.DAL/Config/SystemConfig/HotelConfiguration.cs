using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Base.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Base.DAL.Config.SystemConfig
{
    public class HotelConfiguration : BaseEntityConfigurations<Hotel>
    {
        public override void Configure(EntityTypeBuilder<Hotel> builder)
        {
            base.Configure(builder);

            builder.Property(h => h.Name)
                .HasColumnType(DBTypes.NVARCHAR)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(h => h.Description)
                .HasColumnName("description")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(h => h.Street)
                .HasColumnType(DBTypes.NVARCHAR)
                .HasColumnName("street")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.City)
                .HasColumnType(DBTypes.NVARCHAR)
                .HasColumnName("city")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.Governate)
                .HasColumnName("governate")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(h => h.HotelStatus)
                .HasColumnName("hotel_status")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(6)
                .HasConversion(new EnumToStringConverter<HotelStatus>())
                .IsRequired()
                .HasDefaultValue(HotelStatus.Closed.ToString());

            builder.HasMany(h => h.HotelPhotos)
                .WithOne(p => p.Hotel)
                .HasForeignKey(p=>p.HotelId);

            builder.HasMany(h => h.SeasonalPricings)
                   .WithMany(s => s.Hotels)
                   .UsingEntity(j => j.ToTable("hotel_seasonal_pricings"));
            
            builder.HasOne(h => h.Manager)
                .WithMany(a => a.ManagedHotels)
                .HasForeignKey(h => h.CreatedById);

        }
    }

}
