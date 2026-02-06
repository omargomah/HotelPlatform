using Base.DAL.Models.BaseModels;
using Base.Shared.Enums;

namespace Base.DAL.Models.SystemModels
{
    public class Booking:BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public BookingStatus BookingStatus { get; set; } 
        public double TotalPrice { get; set; }
        public string FawaterakInvoiceId { get; set; } // From ExecutePayment API
        public string UnitId { get; set; }
        public Unit Unit { get; set; }
        public ICollection<PaymentTransaction> Transactions { get; set; } = new HashSet<PaymentTransaction>();
    }

}
