using System.Text.Json.Serialization;

namespace HotelPlatform.Shared.DTOs.PaymentDTOs
{
    public class WebHookCancelDTO
    {
        [JsonPropertyName("hashKey")]
        public string HashKey { get; set; }
        [JsonPropertyName("referenceId")]
        public string ReferenceId { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("paymentMethod")]
        public string PaymentMethod { get; set; }
        [JsonPropertyName("pay_load")]
        public PayLoad PayLoad { get; set; }
        [JsonPropertyName("transactionId")]
        public int TransactionId { get; set; }
        [JsonPropertyName("transactionKey")]
        public string TransactionKey { get; set; }
    }


}
