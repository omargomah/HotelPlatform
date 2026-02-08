using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
}
