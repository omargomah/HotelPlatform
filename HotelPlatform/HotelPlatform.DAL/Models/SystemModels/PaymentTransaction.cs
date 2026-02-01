using HotelPlatform.DAL.Models.BaseModel;
using HotelPlatform.Shared.Enums;

namespace HotelPlatform.DAL.Models.SystemModels
{
    public class PaymentTransaction:BaseEntity
    {
        public string BookingId { get; set; }
        public PaymentMethod PaymentMethod { get; set; } 
        public TransactionPayStatus TransactionPayStatus { get; set; }
        public string FawaterakReferenceNum { get; set; }

        // To store the full response for security audits
        public string RawGatewayResponse { get; set; }
    }

}
