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
            builder.Property(c => c.SSN).HasColumnName("SSN").HasColumnType(DBTypes.NVARCHAR).HasMaxLength(14).IsRequired(false);
            builder.Property(c => c.DOB).HasColumnName("DOB").HasColumnType(DBTypes.DATE).IsRequired(false);
            builder.Property(c => c.Gender).HasColumnName("gender").HasColumnType(DBTypes.NVARCHAR).HasMaxLength(5).HasConversion(new EnumToStringConverter<Gender>()).IsRequired(false);
            builder.Property(c => c.ProfileImageLink).HasColumnName("profile_image_link").HasColumnType(DBTypes.NVARCHARMAX).IsRequired(false);
            
            builder.HasOne(c => c.User)
            .WithOne(u => u.Client)
            .HasForeignKey<Client>(c => c.UserId);

        }
    }

}
