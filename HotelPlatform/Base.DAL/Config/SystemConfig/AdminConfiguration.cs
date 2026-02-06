using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DAL.Config.SystemConfig
{
    public class AdminConfiguration : BaseEntityConfigurations<Admin>
    {
        public override void Configure(EntityTypeBuilder<Admin> builder)
        {
            base.Configure(builder);

            builder.Property(a => a.UserId)
                   .HasColumnName("user_id")
                   .HasColumnType(DBTypes.NVARCHAR_36)
                   .IsRequired();

            builder.HasMany(a => a.ManagedHotels)
                   .WithOne();
        }
    }
    public class HotelConfiguration : BaseEntityConfigurations<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            base.Configure(builder);

            builder.Property(h => h.Name).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(200).IsRequired();
            builder.Property(h => h.Description).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(400);
            builder.Property(h => h.Street).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(100);
            builder.Property(h => h.City).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(100);
            builder.Property(h => h.Governate).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(100);
            builder.Property(h => h.HotelStatus).HasColumnType(DBTypes.INT);

            // Many-to-Many with SeasonalPricing
            builder.HasMany(h => h.SeasonalPricings)
                   .WithMany(s => s.Hotels)
                   .UsingEntity(j => j.ToTable("hotel_seasonal_pricings"));
        }
    }

    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ConfigureCoreProperties();

            builder.Property(u => u.BasePrice).HasColumnType(DBTypes.DECIMAL18_4);
            builder.Property(u => u.RoomNumber).HasColumnType(DBTypes.INT);
            builder.Property(u => u.HotelId).HasColumnType(DBTypes.NVARCHAR_36);

            builder.HasOne(u => u.Hotel)
                   .WithMany(h => h.Units)
                   .HasForeignKey(u => u.HotelId);
        }
    }

}
