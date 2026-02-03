using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HotelPlatform.Shared.DTOs.PaymentDTOs
{

    public class WebHookPaidDTO
    {
        [JsonPropertyName("hashKey")]
        public string HashKey { get; set; }
        [JsonPropertyName("invoice_key")]
        public string InvoiceKey { get; set; }
        [JsonPropertyName("invoice_id")]
        public int InvoiceId { get; set; }
        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; set; }
        [JsonPropertyName("pay_load")]
        public string Payload { get; set; }
        [JsonPropertyName("invoice_status")]
        public string InvoiceStatus { get; set; }
        [JsonPropertyName("referenceNumber")]
        public string ReferenceNumber { get; set; }
        [JsonPropertyName("paidAmount")]
        public int PaidAmount { get; set; }
        [JsonPropertyName("paidCurrency")]
        public string PaidCurrency { get; set; }
        [JsonPropertyName("paidAt")]
        public DateTime PaidAt { get; set; }
        [JsonPropertyName("customerData")]
        public Customerdata CustomerData { get; set; }
        [JsonPropertyName("cardDiscountAmount")]
        public int CardDiscountAmount { get; set; }
        [JsonPropertyName("discountBankCode")]
        public string DiscountBankCode { get; set; }
        [JsonPropertyName("paymentToken")]
        public string PaymentToken { get; set; }
        [JsonPropertyName("commissionCode")]
        public string CommissionCode { get; set; }
        public class Customerdata
        {
            public string customer_unique_id { get; set; }
            public string customer_first_name { get; set; }
            public string customer_last_name { get; set; }
            public string customer_email { get; set; }
            public string customer_phone { get; set; }
        }
    }



}
