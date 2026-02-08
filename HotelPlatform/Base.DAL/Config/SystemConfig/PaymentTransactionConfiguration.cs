using Base.DAL.Config.BaseConfig;
using Base.DAL.Models.SystemModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Base.DAL.Config.SystemConfig
{
    public class PaymentTransactionConfiguration : BaseEntityConfigurations<PaymentTransaction>
    {
        public override void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.BookingId).HasColumnName("booking_id").HasColumnType(DBTypes.NVARCHAR_36);
            builder.Property(b => b.FawaterakInvoiceId).HasColumnName("fawaterak_invoice_id").HasColumnType(DBTypes.NVARCHAR).HasMaxLength(200);

            builder.HasOne(t=> t.Booking) 
                   .WithMany(b => b.Transactions)
                   .HasForeignKey(p => p.BookingId);
            builder.HasOne(t => t.Client)
                .WithMany(b => b.PaymentTransactions)
                .HasForeignKey(p => p.CreatedById);
        }
    }
}
