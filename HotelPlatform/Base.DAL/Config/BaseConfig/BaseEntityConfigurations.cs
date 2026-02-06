using Base.DAL.Models.BaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DAL.Config.BaseConfig
{
    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .HasColumnType(DBTypes.NVARCHAR_36)
                   .ValueGeneratedNever(); 

            builder.Property(e => e.CreatedById)
                   .HasColumnName("created_by_id")
                   .HasColumnType(DBTypes.NVARCHAR_36)
                   .IsRequired(false);

            builder.Property(e => e.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType(DBTypes.DATETIME)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            builder.Property(e => e.UpdatedById)
                   .HasColumnName("updated_by_id")
                   .HasColumnType(DBTypes.NVARCHAR_36)
                   .IsRequired(false);

            builder.Property(e => e.UndatedAt)
                   .HasColumnName("updated_at")
                   .HasColumnType(DBTypes.DATETIME)
                   .IsRequired(false);

            builder.Property(e => e.IsDeleted)
                   .HasColumnName("is_deleted")
                   .HasColumnType(DBTypes.BIT)
                   .HasDefaultValue(false);

        }
    }

}
