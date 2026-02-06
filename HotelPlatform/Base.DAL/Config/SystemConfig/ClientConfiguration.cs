using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Base.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Base.DAL.Config.SystemConfig
{
    public class ClientConfiguration : BaseEntityConfigurations<Client>
    {
        public override void Configure(EntityTypeBuilder<Client> builder)
        {
            base.Configure(builder);

            builder
                .Property(c => c.UserId)
                .HasColumnName("user_id")
                .HasColumnType(DBTypes.NVARCHAR_36)
                .IsRequired();
            builder.Property(c => c.SSN).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(20).IsRequired();
            builder.Property(c => c.DOB).HasColumnType(DBTypes.DATE).IsRequired();
            builder.Property(c => c.Gender).HasColumnType(DBTypes.NVARCHAR).HasMaxLength(5).HasConversion(new EnumToStringConverter<Gender>());
            builder.Property(c => c.ProfileImageLink).HasColumnType(DBTypes.NVARCHARMAX);
        }
    }

}
