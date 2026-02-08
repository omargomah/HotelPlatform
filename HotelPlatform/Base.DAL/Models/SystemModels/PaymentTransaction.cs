using Base.DAL.Models.BaseModels;
using Base.Shared.Enums;

namespace Base.DAL.Models.SystemModels
{
    public class PaymentTransaction:BaseEntity
    {
        public string BookingId { get; set; }
        public string FawaterakInvoiceId { get; set; }
        public PaymentMethodType PaymentMethodType { get; set; } 
        public TransactionPayStatus TransactionPayStatus { get; set; }
        public Booking Booking { get; set; }
        public Client Client { get; set; }
    }

}
