using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelPlatform.Shared.DTOs
{

    public class EInvoiceRequestDTO
    {

        [JsonPropertyName("payment_method_id")]
        public int? PaymentMethodId { get; set; }
        [JsonPropertyName("invoice_number")]
        public int? InvoiceNumber { get; set; }
        [JsonPropertyName("cartTotal")]
        public string CartTotal { get; set; }
        [JsonPropertyName("currency")]
        public string currency { get; set; }
        [JsonPropertyName("sendEmail")]
        public bool SendEmail { get; set; }
        [JsonPropertyName("sendSMS")]
        public bool SendSMS { get; set; }
        [JsonPropertyName("customer")]
        public Customer Customer { get; set; }
        [JsonPropertyName("redirectionUrls")]
        public Redirectionurls RedirectionUrls { get; set; }
        [JsonPropertyName("cartItems")]
        public Cartitem[] CartItems { get; set; }
        [JsonPropertyName("payLoad")]
        public PayLoad PayLoad { get; set; }

    }


}
