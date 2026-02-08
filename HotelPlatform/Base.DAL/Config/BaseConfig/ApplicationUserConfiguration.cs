using Base.DAL.Models.BaseModels;
using Base.DAL.Models.SystemModels;
using Base.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Base.DAL.Config.BaseConfig
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id)
               .HasColumnName("id")
               .HasColumnType(DBTypes.NVARCHAR_36);


            builder.Property(u => u.Type)
                .HasColumnName("user_type")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(5)
                .HasConversion(new EnumToStringConverter<UserTypes>());

            builder.Property(u => u.FullName)
                   .HasColumnName("full_name")
                   .HasColumnType(DBTypes.NVARCHAR)
                   .HasMaxLength(200)
                   .HasComputedColumnSql("[first_name]+' '+[last_name]");
            
            builder.Property(u => u.FName)
                .HasColumnName("first_name")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(u => u.LName)
                .HasColumnName("last_name")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(u => u.Address)
                .HasColumnName("address")
                .HasColumnType(DBTypes.NVARCHAR)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(u => u.IsActive)
                   .HasColumnName("is_active")
                   .HasColumnType(DBTypes.BIT)
                   .HasDefaultValue(true)
                   .IsRequired();



        }
    }
}
