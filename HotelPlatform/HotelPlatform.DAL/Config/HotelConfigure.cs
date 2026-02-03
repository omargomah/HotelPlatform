using HotelPlatform.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelPlatform.DAL.Config
{
    public class HotelConfigure : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasMany(h => h.Units)
                   .WithOne(u => u.Hotel)
                   .HasForeignKey(u => u.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.Reviews)
                   .WithOne(r => r.Hotel)
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(h => h.AdminManagedHotels)
                   .WithOne(amh => amh.Hotel)
                   .HasForeignKey(amh => amh.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
