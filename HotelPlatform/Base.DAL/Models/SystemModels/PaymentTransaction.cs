using Base.DAL.Models.BaseModels;
using Base.Shared.Enums;

namespace Base.DAL.Models.SystemModels
{
    public class PaymentTransaction:BaseEntity
    {
        public string BookingId { get; set; }
        public PaymentMethodType PaymentMethod { get; set; } 
        public TransactionPayStatus TransactionPayStatus { get; set; }
        public string ReferenceNum { get; set; }
        public string InvoiceKey { get; set; }
    }

}
