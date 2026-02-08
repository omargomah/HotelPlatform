using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DAL.Config.SystemConfig
{
    public class UnitConfiguration : BaseEntityConfigurations<Unit>
    {
        public override void Configure(EntityTypeBuilder<Unit> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.BasePrice).HasColumnType(DBTypes.DECIMAL18_4);
            builder.Property(u => u.RoomNumber).HasColumnType(DBTypes.INT);
            builder.Property(u => u.HotelId).HasColumnType(DBTypes.NVARCHAR_36);

            builder.HasOne(u => u.Hotel)
                   .WithMany(h => h.Units)
                   .HasForeignKey(u => u.HotelId);
        }
    }

}
